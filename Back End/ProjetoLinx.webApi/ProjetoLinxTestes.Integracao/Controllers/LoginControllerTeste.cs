using FluentAssertions;
using Newtonsoft.Json;
using ProjetoLinx.webApi.Criptografia;
using ProjetoLinx.webApi.DTO;
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
    public class LoginControllerTeste : TestBase
    {
        private readonly ISenha _senha;
        public LoginControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
            _senha = (ISenha)Scope.ServiceProvider.GetService(typeof(ISenha));
        }

        [Fact]
        public async Task LoginDeveRetornarToken()
        {
            var usuario = new UsuarioBuilder().Generate();
            var login = new LoginBuilder(usuario.Email, usuario.Senha);

            usuario.Senha = _senha.Criptografar(usuario.Senha);
            await Context.Usuarios.AddAsync(usuario);
            await Context.SaveChangesAsync();

            var resposta = await Client.PostAsJsonAsync("Login", login);
            var respostaConteudo = resposta.Content;

            respostaConteudo.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task LoginDeveRetornar401UnathorizedSeEmailUtilizadoNaoExistir()
        {
            var login = new LoginBuilder("emailErrado@email.com", "12345678").Generate();

            var content = JsonConvert.SerializeObject(login);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resposta = await Client.PostAsync("Login", stringContent);

            resposta.Should()
                .Be401Unauthorized();
        }

        [Fact]
        public async Task LoginDeveRetornar401UnathorizedSeSenhaUtilizadoNaoCorresponderASenhaCriptografada()
        {
            var usuario = new UsuarioBuilder().Generate();
            var login = new LoginBuilder(usuario.Email, "SenhaSuperErrada").Generate();
            usuario.Senha = _senha.Criptografar(usuario.Senha);
            await Context.Usuarios.AddAsync(usuario);
            await Context.SaveChangesAsync();

            var content = JsonConvert.SerializeObject(login);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resposta = await Client.PostAsync("Login", stringContent);

            resposta.Should()
                .Be401Unauthorized();
        }
    }
}
