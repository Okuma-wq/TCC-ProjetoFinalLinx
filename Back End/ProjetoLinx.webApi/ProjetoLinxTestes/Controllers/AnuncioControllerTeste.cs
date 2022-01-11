using AutoMapper;
using Moq;
using ProjetoLinx.webApi.Controllers;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Unidade.Controllers
{
    public class AnuncioControllerTeste
    {
        [Fact]
        public async Task BuscarPorIdAnuncioDeveBuscarNoRepositorioComOIdRecebidoComoParametro()
        {
            var id = Guid.NewGuid();
            var mockRepositorioAnuncio = new Mock<IAnuncioRepository>();
            var mockAnuncioMapper = new Mock<IMapper>();
            var controller = new AnuncioController(mockRepositorioAnuncio.Object, mockAnuncioMapper.Object);

            await controller.BuscarPorIdAnuncio(id);

            mockRepositorioAnuncio.Verify(x => x.BuscarPorIdAnuncioComUsuarioAsync(It.Is<Guid>(x => x == id)), Times.Once);
        }
    }
}
