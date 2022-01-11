using FluentAssertions;
using ProjetoLinx.webApi.DTO;
using System;
using Xunit;

namespace ProjetoLinxTestes.Unidade.DTOs
{
    public class CriarReuniaoDTOTeste
    {
        [Fact]
        public void DeveRetornarIsValidSeCriarReuniaoDTOForPreenchidoCorretamente()
        {
            var reuniao = new CriarReuniaoDTO();
            reuniao.FornecedorId = Guid.NewGuid();
            reuniao.LojistaId = Guid.NewGuid();
            reuniao.TituloAnuncio = "TituloTeste";

            reuniao.Validar();

            reuniao.IsValid.Should()
                .BeTrue();
        }


        [Fact]
        public void NotificationDeveRetornarKeysFornecedorIdELojistaIdQuandoEssasPropriedadesForemInvalidas()
        {
            var reuniao = new CriarReuniaoDTO();

            reuniao.Validar();

            reuniao.Notifications.Should()
                .Contain(x => x.Key == "LojistaId")
                .And.Contain(x => x.Key == "FornecedorId")
                .And.Contain(x => x.Key == "TituloAnuncio");
        }
    }
}
