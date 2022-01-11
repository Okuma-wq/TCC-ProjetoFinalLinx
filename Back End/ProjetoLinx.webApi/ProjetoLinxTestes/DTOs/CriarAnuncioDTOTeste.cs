using FluentAssertions;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Unidade.DTOs
{
    public class CriarAnuncioDTOTeste
    {
        [Fact]
        public void DeveRetornarIsValidSeAnuncioCriacaoForPreenchidoCorretamente()
        {
            var anuncio = new CriarAnuncioDTO();
            anuncio.Titulo = "teste";
            anuncio.Descricao = "descrição teste";
            anuncio.Status = StatusAnuncioEnum.Ativo;

            anuncio.Validar();

            anuncio.IsValid.Should()
                .BeTrue();
        }

        [Fact]
        public void NotificationDeveRetornarKeysIdUsuarioTituloDescricaoQuandoEssasPropriedadesForemInvalidas()
        {
            var anuncio = new CriarAnuncioDTO();

            anuncio.Validar();

            anuncio.Notifications.Should()
                .Contain(x => x.Key == "Titulo")
                .And.Contain(x => x.Key == "Descrição");
        }
    }
}
