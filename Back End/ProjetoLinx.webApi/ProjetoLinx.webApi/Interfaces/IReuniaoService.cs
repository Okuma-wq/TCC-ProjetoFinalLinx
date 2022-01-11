using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IReuniaoService
    {
        Task<IEnumerable<ReuniaoComUsuarioDTO>> ListarReunioesDoUsuarioAsync(Guid id, TipoUsuarioEnum tipoUsuario);
        Task<ReuniaoComDetalhesDTO> BuscarPorIdDaReuniao(Guid id, TipoUsuarioEnum tipoUsuario);
        Task<ReuniaoSemEntidadesDTO> AlterarReuniaoAsync(Guid id, ReuniaoComDadosParaAlterarDTO reuniaoAlterada);
        Task<ReuniaoSemEntidadesDTO> AlterarStatusReuniaoAsync(Guid id, StatusReuniaoEnum novoStatus);
    }
}
