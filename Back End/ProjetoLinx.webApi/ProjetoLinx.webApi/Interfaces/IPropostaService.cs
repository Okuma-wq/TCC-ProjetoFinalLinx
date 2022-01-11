using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IPropostaService
    {
        Task<PropostaParaAnuncio> AlterarStatusDaPropostaAsync(Guid id, StatusPropostaAnuncioEnum novoStatus);

        Task<PropostaParaAnuncio> CadastrarPropostaAsync(Guid FornecedorId, Guid AnuncioId);
        Task<IEnumerable<PropostaComUsuarioDTO>> BuscarPropostaPorAnuncioIdAsync(Guid id);
    }
}
