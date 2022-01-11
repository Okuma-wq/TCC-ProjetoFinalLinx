using FluentAssertions;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using Xunit;

namespace ProjetoLinxTestes.Unidade.DTOs
{
    public class UsuarioCriacaoTeste
    {
        [Fact]
        public void DeveRetornarIsValidSeUsuarioCriacaoForPreenchidoCorretamente()
        {
            var usuario = new UsuarioDTO();
            usuario.Nome = "Pedro";
            usuario.Email = "email@email.com";
            usuario.Senha = "12345678";
            usuario.Telefone = "551144014747";
            usuario.Celular = "5511940607612";
            usuario.Cnpj = "26411371000150";
            usuario.RazaoSocial = "Empresa Nova";
            usuario.TipoUsuario = TipoUsuarioEnum.Fornecedor;

            usuario.Validar();

            usuario.IsValid.Should()
                .BeTrue();
        }

        [Fact]
        public void NotificationDeveRetornarKeysNomeEmailSenhaTelefoneCelularCnpjRazaoSocialQuandoEssasPropriedadesForemInvalidas()
        {
            var usuario = new UsuarioDTO();
            usuario.Nome = "";
            usuario.Email = "email@emai";
            usuario.Senha = "1234567";
            usuario.Telefone = "44014747";
            usuario.Celular = "940607612";
            usuario.Cnpj = "264113710001";
            usuario.RazaoSocial = "";
            usuario.TipoUsuario = TipoUsuarioEnum.Fornecedor;

            usuario.Validar();

            usuario.Notifications.Should()
                .Contain(x => x.Key == "Nome")
                .And.Contain(x => x.Key == "Email")
                .And.Contain(x => x.Key == "Senha")
                .And.Contain(x => x.Key == "Telefone")
                .And.Contain(x => x.Key == "Celular")
                .And.Contain(x => x.Key == "Cnpj")
                .And.Contain(x => x.Key == "RazaoSocial");
        }
    }
}
