using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IPropostaRepository
    {
        Task CadastrarAsync(PropostaParaAnuncio proposta);

        Task AlterarStatusAsync(PropostaParaAnuncio propostaAlterada);

        Task<PropostaParaAnuncio> BuscarPorPropostaIdAsync(Guid id);

        Task<IEnumerable<PropostaParaAnuncio>> BuscarPorAnuncioIdAsync(Guid id);

        Task<PropostaParaAnuncio> BuscarPorAnuncioIdEFornecedorId(Guid anuncioId, Guid fornecedorId);
    }
}
