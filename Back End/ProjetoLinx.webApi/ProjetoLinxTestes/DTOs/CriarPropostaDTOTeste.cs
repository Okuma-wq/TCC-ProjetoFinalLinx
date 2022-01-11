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
    public class CriarPropostaDTOTeste
    {
        [Fact]
        public void DeveRetornarIsValidSeCriarPropostaDTOForPreenchidoCorretamente()
        {
            var proposta = new CriarPropostaDTO();
            proposta.AnuncioId = Guid.NewGuid();

            proposta.Validar();

            proposta.IsValid.Should()
                .BeTrue();
        }

        [Fact]
        public void NotificationDeveRetornarKeysAnuncioIdEUsuarioIdQuandoEssasPropriedadesForemInvalidas()
        {
            var proposta = new CriarPropostaDTO();

            proposta.Validar();

            proposta.Notifications.Should()
                .Contain(x => x.Key == "AnuncioId");
        }
    }
}
