using AutoMapper;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains.Services
{
    public class TopicoService : ITopicoService
    {
        private ITopicoRepository _topico { get; }
        private IMapper _mapper { get; }
        private IReuniaoRepository _reuniao { get; }

        public TopicoService(IMapper mapper, ITopicoRepository topico, IReuniaoRepository reuniao)
        {
            _mapper = mapper;
            _topico = topico;
            _reuniao = reuniao;
        }

        public async Task<TopicoSemEntidadeDTO> CadastrarAsync(CriarTopicoDTO novoTopico)
        {
            var topicoMapeado = _mapper.Map<Topico>(novoTopico);
            var reuniao = await _reuniao.BuscarPorIdDaReuniaoAsync(novoTopico.ReuniaoId);

            if (reuniao == null)
                return null;

            await _topico.CadastrarAsync(topicoMapeado);
            var resposta = _mapper.Map<TopicoSemEntidadeDTO>(topicoMapeado);

            return resposta;
        }

        public async Task<IEnumerable<TopicoSemEntidadeDTO>> ListarPorReuniaoIdAsync(Guid reuniaoId)
        {
            return  _mapper.Map<IEnumerable<TopicoSemEntidadeDTO>>(await _topico.ListaTopicosDaReuniaoAsync(reuniaoId));
        }

        public async Task DeletarAsync(Guid id)
        {
            var topicoBuscado = await _topico.BuscarPorId(id);
            
            await _topico.DeletarAsync(topicoBuscado);
        }
    }
}
