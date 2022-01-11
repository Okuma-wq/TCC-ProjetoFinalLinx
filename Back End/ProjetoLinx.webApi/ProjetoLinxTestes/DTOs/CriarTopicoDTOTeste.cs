using FluentAssertions;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Unidade.DTOs
{
    public class CriarTopicoDTOTeste
    {
        [Fact]
        public void DeveRetornarIsValidSeCriarTopicoDTOForPreenchidoCorretamente()
        {
            var topico = new CriarTopicoDTO();
            topico.ReuniaoId= Guid.NewGuid();
            topico.Titulo = "teste";
            

            topico.Validar();

            topico.IsValid.Should()
                .BeTrue();
        }

        [Fact]
        public void NotificationDeveRetornarKeysTituloEmptyEReuniaoIdQuandoEssasPropriedadesForemInvalidas()
        {
            var topico = new CriarTopicoDTO();

            topico.Validar();

            topico.Notifications.Should()
                .Contain(x => x.Key == "TituloEmpty")
                .And.Contain(x => x.Key == "ReuniaoId");
        }

        [Fact]
        public void NotificationDeveRetornarKeyTituloLengthQuandoEssaPropriedadeForInvalida()
        {
            var topico = new CriarTopicoDTO();
            topico.Titulo = "Discutir o orçamento livre e possíveis mudanças na compra desejada";

            topico.Validar();

            topico.Notifications.Should()
                .Contain(x => x.Key == "TituloLength");
        }
    }
}
