using ProjetoLinx.webApi.Domains;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IAnexoRepository
    {
        Task CadastrarAsync(Anexo anexo);
    }
}
