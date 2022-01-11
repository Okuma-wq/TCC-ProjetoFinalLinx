using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface ITopicoService
    {
        Task<TopicoSemEntidadeDTO> CadastrarAsync(CriarTopicoDTO novoTopico);
        Task<IEnumerable<TopicoSemEntidadeDTO>> ListarPorReuniaoIdAsync(Guid reuniaoId);
        Task DeletarAsync(Guid id);
    }
}
