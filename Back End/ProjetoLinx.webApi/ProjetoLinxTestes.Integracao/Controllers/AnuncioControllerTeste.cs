using FluentAssertions;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
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
    public class AnuncioControllerTeste : TestBase
    {
        public ILoginService _loginService;
        public IAnuncioRepository _anuncioRepository;
        public AnuncioControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
            _loginService = (ILoginService)Scope.ServiceProvider.GetService(typeof(ILoginService));
            _anuncioRepository = (IAnuncioRepository)Scope.ServiceProvider.GetService(typeof(IAnuncioRepository));
        }

        private async Task<Usuario> PersistirLojistaNoBanco()
        {
            var lojista = new UsuarioBuilder().ComTipoUsuarioLojista().Generate();
            await Context.Usuarios.AddAsync(lojista);
            await Context.SaveChangesAsync();
            return lojista;
        }

        #region Testes de cadastro

        [Fact]
        public async Task CadastrarDeveRetornar201CreatedESalvarAnuncioNoBanco()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var anuncioEsperado = new CriarAnuncioDTOBuilder().Generate();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var resposta = await Client.PostAsJsonAsync("Anuncio", anuncioEsperado);
           
            var anuncioRetornado = await resposta.Content.ReadFromJsonAsync<AnuncioRespostaDTO>();
            
            var anuncioNoBancoMapeado = Mapper.Map<AnuncioRespostaDTO>(Context.Anuncios.FirstOrDefault(x => x.Id == anuncioRetornado.Id));
            
            resposta.Should()
                .Be201Created();

            anuncioNoBancoMapeado.Should()
                .BeEquivalentTo(anuncioRetornado);
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoAnuncioSejaInvalido()
        {
            var lojista = await PersistirLojistaNoBanco();
            var anuncioEsperado = new CriarAnuncioDTO();


            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var resposta = await Client.PostAsJsonAsync("Anuncio", anuncioEsperado);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoDadosDoAnuncioSejamNulos()
        {
            var lojista = await PersistirLojistaNoBanco();
            var anuncioEsperado = new { };

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var resposta = await Client.PostAsJsonAsync("Anuncio", anuncioEsperado);

            resposta.Should()
                .Be400BadRequest();
        }

        #endregion

        #region Testes de Listar

        [Fact]
        public async Task ListarDeveDevolverTodosOsAnunciosAtivosDoBanco()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(lojista).Generate(3);
            var anunciosEsperados = Mapper.Map<IEnumerable<AnuncioRespostaDTO>>(anunciosCadastrados);
            

            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetFromJsonAsync<IEnumerable<AnuncioRespostaDTO>>("Anuncio");

            var esperado = anunciosEsperados.Where(x => x.Status == ProjetoLinx.webApi.Enums.StatusAnuncioEnum.Ativo);

            resposta.Should()
                .BeEquivalentTo(esperado);
        }

        [Fact]
        public async Task ListarDeveRetornarStatusCodeOk()
        {
            var resposta = await Client.GetAsync("Anuncio");

            resposta.Should()
                .Be200Ok();
        }

        #endregion

        #region Testes de BuscarPorIdAnuncio

        [Fact]
        public async Task BuscarPorIdAnuncioDeveRetornarAnuncioBuscadoDoBanco()
        {
            Usuario usuario = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(usuario).Generate();
            var anuncioEsperado = Mapper.Map<AnuncioComUsuarioDTO>(anunciosCadastrados);


            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetFromJsonAsync<AnuncioComUsuarioDTO>($"Anuncio/{anuncioEsperado.Id}");

            resposta.Should()
                .BeEquivalentTo(anuncioEsperado);
        }

        [Fact]
        public async Task BuscarPorIdAnuncioDeveRetornarStatusCode404CasoIdNaoEstejaNoBanco()
        {
            var id = Guid.NewGuid();

            var resposta = await Client.GetAsync($"Anuncio/{id}");

            resposta.Should()
                .Be404NotFound();
        }

        [Fact]
        public async Task BuscarPorIdAnuncioDeveRetornar400BadRequestCasoParamentroNaoSejaUmGuidValido()
        {
            var id = new Random().Next();

            var resposta = await Client.GetAsync($"Anuncio/{id}");

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task BuscarPorIdAnuncioDeverRetornar200Ok()
        {
            Usuario usuario = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(usuario).Generate();
            var anuncioEsperado = Mapper.Map<AnuncioComUsuarioDTO>(anunciosCadastrados);


            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetAsync($"Anuncio/{anuncioEsperado.Id}");

            resposta.Should()
                .Be200Ok();
        }

        #endregion

        #region Testes de BuscarPorTitulo

        [Fact]
        public async Task BuscarPorTituloDeveDevolverTodosOsAnunciosAtivosDoBanco()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(lojista).Generate(3);
            var anunciosEsperados = Mapper.Map<IEnumerable<AnuncioRespostaDTO>>(anunciosCadastrados);


            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetFromJsonAsync<IEnumerable<AnuncioRespostaDTO>>("Anuncio");

            var esperado = anunciosEsperados.Where(x => x.Status == ProjetoLinx.webApi.Enums.StatusAnuncioEnum.Ativo);

            resposta.Should()
                .BeEquivalentTo(esperado);
        }

        [Fact]
        public async Task BuscarPorTituloDeveRetornarStatusCodeOk()
        {
            var resposta = await Client.GetAsync("Anuncio");

            resposta.Should()
                .Be200Ok();
        }

        #endregion

        #region Testes de Deletar

        [Fact]
        public async Task DeletarDeveRetornarOk()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(lojista).Ativo().Generate();

            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.DeleteAsync($"Anuncio/{anunciosCadastrados.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task DeletarDeveRetornarNullSeOStatusDoAnuncioNoBancoFoiAlteradoParaDeletado()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var anunciosCadastrados = new AnuncioBuilder().ComUsuario(lojista).Generate();

            await Context.Anuncios.AddRangeAsync(anunciosCadastrados);
            await Context.SaveChangesAsync();

            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            await Client.DeleteAsync($"Anuncio/{anunciosCadastrados.Id}");

            var anuncioNoBanco = await _anuncioRepository.BuscarPorIdAnuncioSemUsuarioAsync(anunciosCadastrados.Id);

            anuncioNoBanco.Should()
                .BeNull();
        }

        [Fact]
        public async Task DeletarDeveRetornarNotFoundSeNaoEncontrarOAnuncioParaDeletar()
        {
            Usuario lojista = await PersistirLojistaNoBanco();
            var token = _loginService.GerarToken(lojista);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.DeleteAsync($"Anuncio/{Guid.NewGuid()}");

            resposta.Should()
                .Be404NotFound();
        }
        #endregion
    }
}
