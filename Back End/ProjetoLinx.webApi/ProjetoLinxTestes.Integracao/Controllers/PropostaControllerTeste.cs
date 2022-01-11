using AutoMapper;
using FluentAssertions;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using ProjetoLinxTestes.Integracao.Fakers;
using ProjetoLinxTestes.Integracao.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Integracao.Controllers
{
    public class PropostaControllerTeste : TestBase
    {
        public ILoginService _loginService;
        public IMapper _mapper;
        public PropostaControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
            _loginService = (ILoginService)Scope.ServiceProvider.GetService(typeof(ILoginService));
            _mapper = (IMapper)Scope.ServiceProvider.GetService(typeof(IMapper));
        }

        private async Task<(Usuario, Usuario)> PersistirUsuariosNoBanco()
        {
            var fornecedor = new UsuarioBuilder().ComTipoUsuarioFornecedor().Generate();
            var lojista = new UsuarioBuilder().ComTipoUsuarioLojista().Generate();
            await Context.Usuarios.AddAsync(fornecedor);
            await Context.Usuarios.AddAsync(lojista);
            await Context.SaveChangesAsync();
            return (fornecedor, lojista);
        }

        private async Task<Anuncio> PersistirAnuncioNoBanco(Usuario lojista)
        {
            var anuncio = new AnuncioBuilder().ComUsuario(lojista).Generate();
            await Context.Anuncios.AddAsync(anuncio);
            await Context.SaveChangesAsync();
            return anuncio;
        }

        #region Testes de cadastro

        [Fact]
        public async Task CadastrarDeveRetornar201CreatedSeConseguirCadastrarNovaProposta()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var novaProposta = new CriarPropostaDTO();
            novaProposta.AnuncioId = anuncio.Id;

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Proposta", novaProposta);

            resposta.Should()
                .Be201Created();
        }

        [Fact]
        public async Task CadastrarDeveSalvarAPropostaNoBanco()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var novaProposta = new CriarPropostaDTO();
            novaProposta.AnuncioId = anuncio.Id;

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Proposta", novaProposta);

            var propostaCadastrada = await resposta.Content.ReadFromJsonAsync<PropostaSemEntidadesDTO>();
            var propostaNoBanco = Mapper.Map<PropostaSemEntidadesDTO>(Context.Propostas.FirstOrDefault(x => x.Id == propostaCadastrada.Id));

            propostaCadastrada.Should()
                .BeEquivalentTo(propostaNoBanco);
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoPropostaSejaInvalida()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var proposta= new CriarPropostaDTO();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var resposta = await Client.PostAsJsonAsync("Proposta", proposta);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoDadosDaPropostaSejamNulos()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var proposta = new { };

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var resposta = await Client.PostAsJsonAsync("Proposta", proposta);

            resposta.Should()
                .Be400BadRequest();
        }

        #endregion

        #region Teste de AlterarProposta

        [Fact]
        public async Task AlterarStatusDeverSalvarAlteracoesNoBanco()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var proposta = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate();

            await Context.Propostas.AddAsync(proposta);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Proposta/{proposta.Id}/{StatusPropostaAnuncioEnum.Aceito}");

            var propostaRetornada = await resposta.Content.ReadFromJsonAsync<PropostaSemEntidadesDTO>();

            var propostaEsperada = Mapper.Map<PropostaSemEntidadesDTO>(proposta);
            propostaEsperada.Status = StatusPropostaAnuncioEnum.Aceito;


            propostaEsperada.Should()
                .BeEquivalentTo(propostaRetornada);
        }

        [Fact]
        public async Task AlterarStatusDeveRetornar200Ok()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var proposta = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate();

            await Context.Propostas.AddAsync(proposta);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Proposta/{proposta.Id}/{StatusPropostaAnuncioEnum.Aceito}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarStatusDeveRetornar400BadRequestSePropostaBuscadaForNull()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var proposta = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate();

            await Context.Propostas.AddAsync(proposta);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Proposta/{Guid.Empty}/{StatusPropostaAnuncioEnum.Aceito}");

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task AlterarStatusDeveRetornar400BadRequestSeNovoStatusForPendente()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var proposta = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate();

            await Context.Propostas.AddAsync(proposta);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Proposta/{proposta.Id}/{StatusPropostaAnuncioEnum.Pendente}");

            resposta.Should()
                .Be400BadRequest();
        }

        [Theory]
        [InlineData(StatusPropostaAnuncioEnum.Aceito)]
        [InlineData(StatusPropostaAnuncioEnum.Recusado)]
        public async Task AlterarStatusDeveRetornar400BadRequestSeStatusDaPropostaNoBancoForDiferenteDePendente(StatusPropostaAnuncioEnum status)
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var proposta = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).StatusDiferenteDePendente(status).Generate();

            await Context.Propostas.AddAsync(proposta);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Proposta/{proposta.Id}/{StatusPropostaAnuncioEnum.Recusado}");

            resposta.Should()
                .Be400BadRequest();
        }
        #endregion

        #region BuscarPorAnuncioId

        [Fact]
        public async Task BuscarPorAnuncioIdDeveRetornarOk()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var propostas = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate(3);

            await Context.Propostas.AddRangeAsync(propostas);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Proposta/{anuncio.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task BuscarPorAnuncioIdDeveRetornarPropostasNoBanco()
        {
            var (fornecedor, lojista) = await PersistirUsuariosNoBanco();
            var anuncio = await PersistirAnuncioNoBanco(lojista);
            var propostas = new PropostaParaAnuncioBuilder(fornecedor.Id, anuncio.Id).Generate(3);

            await Context.Propostas.AddRangeAsync(propostas);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<IEnumerable<PropostaComUsuarioDTO>>($"Proposta/{anuncio.Id}");

            var propostasEsperadas = _mapper.Map<IEnumerable<PropostaComUsuarioDTO>>(propostas);
            

            resposta.Should()
                .BeEquivalentTo(propostasEsperadas);
        }
        #endregion
    }
}
