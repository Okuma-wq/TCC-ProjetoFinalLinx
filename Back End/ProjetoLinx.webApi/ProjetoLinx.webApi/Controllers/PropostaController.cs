using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using ProjetoLinx.webApi.Models;
using ProjetoLinx.webApi.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaController : ControllerBase
    {
        private IMapper _mapper { get; }
        private IPropostaService _proposta { get; }


        public PropostaController(IMapper mapper, IPropostaService proposta)
        {
            _mapper = mapper;
            _proposta = proposta;
        }

        [Authorize(Roles = "Fornecedor")]
        [HttpPost]
        public async Task<ActionResult> CadastrarAsync(CriarPropostaDTO criarProposta)
        {
            var fornecedorId = Guid.Parse(HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value);
            criarProposta.Validar();
            if (!criarProposta.IsValid)
                return BadRequest(criarProposta.Notifications);

            var propostaCadastrada = await _proposta.CadastrarPropostaAsync(fornecedorId, criarProposta.AnuncioId);

            if (propostaCadastrada == null)
                return StatusCode(303);

            var propostaResposta = _mapper.Map<PropostaSemEntidadesDTO>(propostaCadastrada);

            return Created("", propostaResposta);
        }

        [HttpGet("{id}/{novoStatus}")]
        public async Task<ActionResult<PropostaSemEntidadesDTO>> AlterarStatus(Guid id, StatusPropostaAnuncioEnum novoStatus)
        {
            var propostaAlterada = await _proposta.AlterarStatusDaPropostaAsync(id, novoStatus);

            if (propostaAlterada == null)
                return BadRequest("Não foi possivel completar a requisição");

            var resposta = _mapper.Map<PropostaSemEntidadesDTO>(propostaAlterada);

            if(novoStatus == StatusPropostaAnuncioEnum.Recusado)
                return Ok();

            return Redirect("http://18.209.203.49/");
        }

        [Authorize(Roles = "Lojista")]
        [HttpGet("{anuncioId}")]
        public async Task<ActionResult<IEnumerable<PropostaComUsuarioDTO>>> BuscarPorAnuncioIdAsync(Guid anuncioId)
        {
            var propostasBuscadas = await _proposta.BuscarPropostaPorAnuncioIdAsync(anuncioId);

            return Ok(propostasBuscadas);
        }
    }
}
