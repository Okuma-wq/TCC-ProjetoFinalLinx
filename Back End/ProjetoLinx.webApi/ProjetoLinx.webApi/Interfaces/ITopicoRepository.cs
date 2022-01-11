using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface ITopicoRepository
    {
        Task CadastrarAsync(Topico novoTopico);
        Task<IEnumerable<Topico>> ListaTopicosDaReuniaoAsync(Guid reuniaoId);
        Task DeletarAsync(Topico topico);
        Task<Topico> BuscarPorId(Guid id);
    }
}
