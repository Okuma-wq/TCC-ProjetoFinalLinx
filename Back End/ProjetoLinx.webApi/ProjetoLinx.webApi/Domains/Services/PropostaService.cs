using AutoMapper;
using ProjetoLinx.webApi.Services;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains.Services
{
    public class PropostaService : IPropostaService
    {
        private IPropostaRepository _proposta { get; }
        private IReuniaoRepository _reuniao { get; }
        private IMapper _mapper { get; }
        private IMailService _mailService { get; }
        private IAnuncioRepository _anuncio { get; }
        private IUsuarioRepository _usuario{ get; }

        public PropostaService(IPropostaRepository proposta, IReuniaoRepository reuniao, IMapper mapper, IMailService mailService, IAnuncioRepository anuncio, IUsuarioRepository usuario)
        {
            _proposta = proposta;
            _reuniao = reuniao;
            _mapper = mapper;
            _mailService = mailService;
            _anuncio = anuncio;
            _usuario = usuario;
        }

        public async Task<PropostaParaAnuncio> AlterarStatusDaPropostaAsync(Guid id, StatusPropostaAnuncioEnum novoStatus)
        {
            var propostaBuscada = await _proposta.BuscarPorPropostaIdAsync(id);

            if (propostaBuscada == null || propostaBuscada.Status != StatusPropostaAnuncioEnum.Pendente || novoStatus == StatusPropostaAnuncioEnum.Pendente)
                return null ;

            propostaBuscada.Status = novoStatus;

            await _proposta.AlterarStatusAsync(propostaBuscada);

            if (novoStatus == StatusPropostaAnuncioEnum.Aceito)
            {
                var novaReuniao = new CriarReuniaoDTO();
                novaReuniao.FornecedorId = Guid.Parse(propostaBuscada.FornecedorId.ToString());
                novaReuniao.LojistaId = propostaBuscada.Anuncio.IdUsuario;
                novaReuniao.TituloAnuncio = propostaBuscada.Anuncio.Titulo;

                novaReuniao.Validar();

                var reuniaoMapeada = _mapper.Map<Reuniao>(novaReuniao);
                reuniaoMapeada.Local = "A ser definido";

                await _reuniao.CadastarAsync(reuniaoMapeada);

                return propostaBuscada;
            }

            return propostaBuscada;
        }

        public async Task<PropostaParaAnuncio> CadastrarPropostaAsync(Guid fornecedorId, Guid anuncioId)
        {
            if (await _proposta.BuscarPorAnuncioIdEFornecedorId(anuncioId, fornecedorId) != null)
                return null;
            
            var proposta = new PropostaParaAnuncio();
            proposta.AnuncioId = anuncioId;
            proposta.FornecedorId = fornecedorId;


            await _proposta.CadastrarAsync(proposta);

            var anuncio = await _anuncio.BuscarPorIdAnuncioComUsuarioAsync(anuncioId);

            var usuario = await _usuario.BuscarPorIdAsync(anuncio.IdUsuario);

            await _mailService.SendAlertEmail(usuario.Email, proposta.Id, anuncio.Titulo, usuario.Nome, usuario.RazaoSocial);

            return proposta;
        }

        public async Task<IEnumerable<PropostaComUsuarioDTO>> BuscarPropostaPorAnuncioIdAsync(Guid id)
        {
            var propostasEncontradas = await _proposta.BuscarPorAnuncioIdAsync(id);

            return _mapper.Map<IEnumerable<PropostaComUsuarioDTO>>(propostasEncontradas);
        }
    }
}
