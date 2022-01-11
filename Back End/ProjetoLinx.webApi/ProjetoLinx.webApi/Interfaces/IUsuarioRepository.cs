using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IUsuarioRepository
    {
        Task CadastrarAsync(Usuario usuario);

        Task<IEnumerable<Usuario>> ListarAsync();

        Task<Usuario> BuscarPorEmailAsync(string email);

        Task<Usuario> BuscarPorIdAsync(Guid id);
        Task<Usuario> BuscarPorNomeERazaoSocial(string nome, string razaoSocial);
    }
}
