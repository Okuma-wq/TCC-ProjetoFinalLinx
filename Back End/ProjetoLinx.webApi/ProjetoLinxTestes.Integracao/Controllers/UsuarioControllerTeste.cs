using FluentAssertions;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
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
    public class UsuarioControllerTeste : TestBase
    {
        public UsuarioControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        #region Testes de cadastro

        [Fact]
        public async Task CadastrarDeveRetornar201CreatedESalvarUsuarioNoBanco()
        {
            var usuario = new UsuarioDTOBuilder().Generate();

            var resposta = await Client.PostAsJsonAsync("Usuario", usuario);

            var usuarioRetornado = await resposta.Content.ReadFromJsonAsync<Usuario>();

            var UsuarioNoBanco = Context.Usuarios.FirstOrDefault(x => x.Id == usuarioRetornado.Id);

            resposta.Should()
                .Be201Created();

            UsuarioNoBanco.Should()
                .BeEquivalentTo(usuarioRetornado);
        }

        [Fact]
        public async Task SenhaCadastradapeloUsuarioDeveSerDiferenteDaSenhaCryptografadaRetornada()
        {
            var usuario = new UsuarioDTOBuilder().Generate();

            var resposta = await Client.PostAsJsonAsync("Usuario", usuario);

            var usuarioRetornado = await resposta.Content.ReadFromJsonAsync<Usuario>();

            var usuarioNoBanco = Context.Usuarios.FirstOrDefault(x => x.Id == usuarioRetornado.Id);

            usuario.Senha.Should()
                .NotBeEquivalentTo(usuarioNoBanco.Senha);
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoUsuarioSejaInvalido()
        {
            var usuarioEsperado = new UsuarioDTOBuilder().Generate();

            usuarioEsperado.Nome = "";

            var resposta = await Client.PostAsJsonAsync("Usuario", usuarioEsperado);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestCasoDadosDoUsuarioSejamNulos()
        {
            var usuarioEsperado = new { };

            var resposta = await Client.PostAsJsonAsync("Usuario", usuarioEsperado);

            resposta.Should()
                .Be400BadRequest();
        }

        #endregion

        #region Testes de Listar

        [Fact]
        public async Task ListarDeveDevolverTodosOsUsuariosDoBanco()
        {
            var usuariosEsperados = new UsuarioBuilder().Generate(4);

            await Context.Usuarios.AddRangeAsync(usuariosEsperados);
            await Context.SaveChangesAsync();

            var resposta = await Client.GetFromJsonAsync<IEnumerable<Usuario>>("Usuario");

            resposta.Should()
                .BeEquivalentTo(usuariosEsperados);
        }

        [Fact]
        public async Task ListarDeveRetornarStatusCodeOk()
        {
            var resposta = await Client.GetAsync("Usuario");

            resposta.Should()
                .Be200Ok();
        }

        #endregion
    }
}
