using AutoMapper;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains.Services
{
    public class ReuniaoService : IReuniaoService
    {
        private IReuniaoRepository _reuniao { get; }
        private IMapper _mapper { get; }

        public ReuniaoService(IMapper mapper, IReuniaoRepository reuniao)
        {
            _mapper = mapper;
            _reuniao = reuniao;
        }

        public async Task<IEnumerable<ReuniaoComUsuarioDTO>> ListarReunioesDoUsuarioAsync(Guid id, TipoUsuarioEnum tipoUsuario)
        {
            if (tipoUsuario == TipoUsuarioEnum.Lojista)
                return _mapper.Map<IEnumerable<ReuniaoComUsuarioDTO>>(await _reuniao.ListarReunioesDoLojistaAsync(id));

            else
                return _mapper.Map<IEnumerable<ReuniaoComUsuarioDTO>>(await _reuniao.ListarReunioesDoFornecedorAsync(id));
        }

        public async Task<ReuniaoComDetalhesDTO> BuscarPorIdDaReuniao(Guid id, TipoUsuarioEnum tipoUsuario)
        {
            Reuniao resposta;

            if (tipoUsuario == TipoUsuarioEnum.Lojista)
                resposta = await _reuniao.BuscarPorIdDaReuniaoDoLojistaAsync(id);
            else
                resposta = await _reuniao.BuscarPorIdDaReuniaoDoFornecedorAsync(id);

            if (resposta == null)
                return null;

            var reuniao = _mapper.Map<ReuniaoComDetalhesDTO>(resposta);
            return reuniao;
        }

        // Adicionar envio de email para a pessoa 
        public async Task<ReuniaoSemEntidadesDTO> AlterarReuniaoAsync(Guid id, ReuniaoComDadosParaAlterarDTO reuniaoAlterada)
        {
            var reuniaoBuscada = await _reuniao.BuscarPorIdDaReuniaoAsync(id);
            if (reuniaoBuscada == null)
                return null;

            if(reuniaoBuscada.DataReuniao != reuniaoAlterada.DataReuniao)
            {
                var valides = reuniaoAlterada.ValidarDataReuniao();
                if (valides == true)
                    return null;
            }

            var reuniaoMapeada = _mapper.Map<Reuniao>(reuniaoAlterada);
            

            reuniaoMapeada.Id = id;
            reuniaoMapeada.FornecedorId = reuniaoBuscada.FornecedorId;
            reuniaoMapeada.LojistaId = reuniaoBuscada.LojistaId;
            reuniaoMapeada.TituloAnuncio = reuniaoBuscada.TituloAnuncio;
            reuniaoMapeada.Status = reuniaoBuscada.Status;

            await _reuniao.AlterarAsync(reuniaoMapeada);

            var resposta = _mapper.Map<ReuniaoSemEntidadesDTO>(reuniaoMapeada);

            return resposta;
        }

        public async Task<ReuniaoSemEntidadesDTO> AlterarStatusReuniaoAsync(Guid id, StatusReuniaoEnum novoStatus)
        {
            var reuniaoBuscada = await _reuniao.BuscarPorIdDaReuniaoAsync(id);
            if (reuniaoBuscada == null || reuniaoBuscada.Status != StatusReuniaoEnum.Ativo)
                return null;

            reuniaoBuscada.Status = novoStatus;

            await _reuniao.AlterarAsync(reuniaoBuscada);

            return _mapper.Map<ReuniaoSemEntidadesDTO>(reuniaoBuscada);
        }
    }
}
