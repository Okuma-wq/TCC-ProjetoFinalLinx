using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IAnuncioRepository
    {
        Task CadastrarAsync(Anuncio anuncio);

        Task<IEnumerable<Anuncio>> ListarAtivosAsync();

        Task<Anuncio> BuscarPorIdAnuncioComUsuarioAsync(Guid id);

        Task<IEnumerable<Anuncio>> BuscarPorTituloAsync(string titulo);

        Task DeletarPorId(Anuncio anuncio);

        Task<Anuncio> BuscarPorIdAnuncioSemUsuarioAsync(Guid id);
    }
}
