using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using ProjetoLinxTestes.Integracao.Fakers;
using ProjetoLinxTestes.Integracao.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Integracao.Controllers
{
    public class ReuniaoControllerTeste : TestBase
    {
        public ILoginService _loginService;
        public IReuniaoRepository _reuniaoRepository;
        public IMapper _mapper;
        public ReuniaoControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
            _loginService = (ILoginService)Scope.ServiceProvider.GetService(typeof(ILoginService));
            _reuniaoRepository = (IReuniaoRepository)Scope.ServiceProvider.GetService(typeof(IReuniaoRepository));
            _mapper = (IMapper)Scope.ServiceProvider.GetService(typeof(IMapper));
        }

        private async Task<(Usuario, Usuario)> CriarFornecedorELojistaNoBanco()
        {
            var fornecedor = new UsuarioBuilder().ComTipoUsuarioFornecedor().Generate();
            var lojista = new UsuarioBuilder().ComTipoUsuarioLojista().Generate();
            await Context.Usuarios.AddAsync(fornecedor);
            await Context.Usuarios.AddAsync(lojista);
            await Context.SaveChangesAsync();
            return (fornecedor, lojista);
        }

        #region ListarReuniaoPorIdDoFornecedorLogado
        [Fact]
        public async Task ListarReuniaoPorIdDoFornecedorLogadoDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync("Reuniao/Minhas");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task ListarReuniaoPorIdDoFornecedorLogadoDeveRetornarReunioesSalvasNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reunioes = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate(5);

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddRangeAsync(reunioes);
            await Context.SaveChangesAsync();

            var reunioesEsperadas = _mapper.Map<IEnumerable<ReuniaoComUsuarioDTO>>(reunioes);

            foreach (var item in reunioesEsperadas)
            {
                item.Nome = lojista.Nome;
                item.RazaoSocial = lojista.RazaoSocial;
            }

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<IEnumerable<ReuniaoComUsuarioDTO>>("Reuniao/Minhas");

            resposta.Should()
                .BeEquivalentTo(reunioesEsperadas);
        }

        #endregion

        #region ListaReuniaoPorIdDoLojistaLogado
        [Fact]
        public async Task ListarReuniaoPorIdDoLojistaLogadoDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync("Reuniao/Minhas");

            resposta.Should()
                .Be200Ok();
        }


        [Fact]
        public async Task ListarReuniaoPorIdDoLojistaLogadoDeveRetornarReunioesSalvasNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reunioes = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate(5);

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddRangeAsync(reunioes);
            await Context.SaveChangesAsync();

            foreach (var item in reunioes)
            {
                item.Fornecedor = fornecedor;
            }

            var reunioesEsperadas = _mapper.Map<IEnumerable<ReuniaoComUsuarioDTO>>(reunioes);

            foreach (var item in reunioesEsperadas)
            {
                item.Nome = fornecedor.Nome;
                item.RazaoSocial = fornecedor.RazaoSocial;
            }

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<IEnumerable<ReuniaoComUsuarioDTO>>("Reuniao/Minhas");

            resposta.Should()
                .BeEquivalentTo(reunioesEsperadas);
        }
        #endregion

        #region BuscarReuniaoPorId (Lojista)

        [Fact]
        public async Task BuscarReuniaoPorIdDoLojistaDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/{reuniao.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoLojistaDeveRetornar404NotFoundSeIdNaoExistirNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/{Guid.NewGuid()}");

            resposta.Should()
                .Be404NotFound();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoLojistaDeveRetornar400BadRequestSeReceberAlgoDiferenteDeGuid()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/1");

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoLojistaDeveRetornarReuniaoSalvaNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<ReuniaoSemEntidadesDTO>($"Reuniao/Minhas/{reuniao.Id}");
            var reuniaoEsperada = _mapper.Map<ReuniaoSemEntidadesDTO>(reuniao);

            resposta.Should()
                .BeEquivalentTo(reuniaoEsperada);
        }


        #endregion

        #region BuscarReuniaoPorId (Fornecedor)

        [Fact]
        public async Task BuscarReuniaoPorIdDoFornecedorDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/{reuniao.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoFornecedorDeveRetornar404NotFoundSeIdNaoExistirNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/{Guid.NewGuid()}");

            resposta.Should()
                .Be404NotFound();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoFornecedorDeveRetornar400BadRequestSeReceberAlgoDiferenteDeGuid()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Reuniao/Minhas/1");

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task BuscarReuniaoPorIdDoFornecedorDeveRetornarReuniaoSalvaNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<ReuniaoSemEntidadesDTO>($"Reuniao/Minhas/{reuniao.Id}");
            var reuniaoEsperada = _mapper.Map<ReuniaoSemEntidadesDTO>(reuniao);

            resposta.Should()
                .BeEquivalentTo(reuniaoEsperada);
        }


        #endregion

        #region AlterarReuniao
        
        [Fact]
        public async Task AlterarReuniaoDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = new ReuniaoComDadosParaAlterarDTO();
            alteracoes.DataReuniao = DateTime.Now.AddDays(12);
            alteracoes.Local = "Local de teste";

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/Alterar", stringContent);

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarReuniaoDeveRetornar200OkMesmoSemUmaNovaDataReuniao()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = new ReuniaoComDadosParaAlterarDTO();
            alteracoes.DataReuniao = reuniao.DataReuniao;
            alteracoes.Local = "Local de teste";

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/Alterar", stringContent);

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarReuniaoDeveSalvarAlteraçõesNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = new ReuniaoComDadosParaAlterarDTO();
            alteracoes.DataReuniao = DateTime.Now.AddDays(12);
            alteracoes.Local = "Local de teste";

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/Alterar", stringContent);
            var reuniaoAlterada = await resposta.Content.ReadFromJsonAsync<ReuniaoSemEntidadesDTO>();
            var reuniaoNoBanco = _mapper.Map<ReuniaoSemEntidadesDTO>(Context.Reunioes.AsNoTracking().FirstOrDefault(x => x.Id == reuniao.Id));

            reuniaoAlterada.Should()
                .BeEquivalentTo(reuniaoNoBanco);
        }

        [Fact]
        public async Task AlterarReuniaoDeveRetornar400BadRequestSeFaltarMenosDeUmaSemanaParaNovaDataReuniao()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = new ReuniaoComDadosParaAlterarDTO();
            alteracoes.DataReuniao = DateTime.Now.AddDays(6);
            alteracoes.Local = "Local de teste";

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/Alterar", stringContent);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task AlterarReuniaoDeveRetornar400BadRequestSeOIdBuscadoNaoRetornarUmaReuniao()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = new ReuniaoComDadosParaAlterarDTO();
            alteracoes.DataReuniao = DateTime.Now.AddDays(6);
            alteracoes.Local = "Local de teste";

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{Guid.NewGuid()}/Alterar", stringContent);

            resposta.Should()
                .Be400BadRequest();
        }



        #endregion

        #region AlterarStatusReuniao

        [Fact]
        public async Task AlterarStatusReuniaoParaConcluidaDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = StatusReuniaoEnum.Concluida;

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/AlterarStatus", stringContent);

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarStatusReuniaoParaCanceladaDeveRetornarOk()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = StatusReuniaoEnum.Cancelada;

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/AlterarStatus", stringContent);

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarStatusReuniaoDeveSalvarAlteraçõesNoBanco()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = StatusReuniaoEnum.Concluida;

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/AlterarStatus", stringContent);
            var reuniaoAlterada = await resposta.Content.ReadFromJsonAsync<ReuniaoSemEntidadesDTO>();
            var reuniaoNoBanco = _mapper.Map<ReuniaoSemEntidadesDTO>(Context.Reunioes.AsNoTracking().FirstOrDefault(x => x.Id == reuniao.Id));

            reuniaoAlterada.Should()
                .BeEquivalentTo(reuniaoNoBanco);
        }

        [Fact]
        public async Task AlterarStatusReuniaoDeveRetornar400BadRequestSeReuniaoBuscadaForNula()
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = StatusReuniaoEnum.Cancelada;

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{Guid.NewGuid()}/AlterarStatus", stringContent);

            resposta.Should()
                .Be400BadRequest();
        }

        [Theory]
        [InlineData(StatusReuniaoEnum.Cancelada)]
        [InlineData(StatusReuniaoEnum.Concluida)]
        public async Task AlterarStatusReuniaoDeveRetornar400BadRequestSeStatusDaReuniaoBuscadaForDiferenteDeAtivo(StatusReuniaoEnum status)
        {
            var (fornecedor, lojista) = await CriarFornecedorELojistaNoBanco();
            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).ComStatus(status).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();

            var alteracoes = StatusReuniaoEnum.Cancelada;

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PatchAsync($"Reuniao/Minhas/{reuniao.Id}/AlterarStatus", stringContent);

            resposta.Should()
                .Be400BadRequest();
        }

        #endregion
    }
}
