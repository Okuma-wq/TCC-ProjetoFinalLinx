using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IReuniaoRepository
    {
        Task CadastarAsync(Reuniao novaReuniao);
        Task<IEnumerable<Reuniao>> ListarReunioesDoFornecedorAsync(Guid Id);
        Task<IEnumerable<Reuniao>> ListarReunioesDoLojistaAsync(Guid Id);
        Task<Reuniao> BuscarPorIdDaReuniaoAsync(Guid id);
        Task<Reuniao> BuscarPorIdDaReuniaoDoLojistaAsync(Guid id);
        Task<Reuniao> BuscarPorIdDaReuniaoDoFornecedorAsync(Guid id);
        Task AlterarAsync(Reuniao reuniaoAlterada);
    }
}
