using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReuniaoController : ControllerBase
    {
        private IReuniaoService _reuniao { get; }

        public ReuniaoController(IReuniaoService reuniao)
        {
            _reuniao = reuniao;
        }

        [Authorize]
        [HttpGet("Minhas")]
        public async Task<ActionResult<IEnumerable<ReuniaoComUsuarioDTO>>> ListarReunioesDoUsuarioAsync()
        {
            var id = Guid.Parse(HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value);
            var tipo = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            TipoUsuarioEnum tipoUsuario = (TipoUsuarioEnum)Enum.Parse(typeof(TipoUsuarioEnum), tipo);

            var va = await _reuniao.ListarReunioesDoUsuarioAsync(id, tipoUsuario);

            return Ok(va);
        }

        [Authorize]
        [HttpGet("Minhas/{id}")]
        public async Task<ActionResult<ReuniaoComDetalhesDTO>> BuscarReuniaoPorIdAsync(Guid id)
        {
            var tipo = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            TipoUsuarioEnum tipoUsuario = (TipoUsuarioEnum)Enum.Parse(typeof(TipoUsuarioEnum), tipo);
            var resposta = await _reuniao.BuscarPorIdDaReuniao(id, tipoUsuario);

            if (resposta == null)
                return NotFound();

            return Ok(resposta);
        }

        [Authorize]
        [HttpPatch("Minhas/{id}/Alterar")]
        public async Task<ActionResult<ReuniaoSemEntidadesDTO>> AlterarReuniaoAsync(Guid id, ReuniaoComDadosParaAlterarDTO reuniaoAlterada)
        {
            var resposta = await _reuniao.AlterarReuniaoAsync(id, reuniaoAlterada);

            if (resposta == null)
                return BadRequest();

            return Ok(resposta);
        }

        [Authorize]
        [HttpPatch("Minhas/{id}/AlterarStatus")]
        public async Task<ActionResult<ReuniaoSemEntidadesDTO>> AlterarStatusReuniaoAsync(Guid id, [FromBody] StatusReuniaoEnum novoStatus)
        {
            var resposta = await _reuniao.AlterarStatusReuniaoAsync(id, novoStatus);

            if (resposta == null)
                return BadRequest();

            return Ok(resposta);
        }
    }
}
