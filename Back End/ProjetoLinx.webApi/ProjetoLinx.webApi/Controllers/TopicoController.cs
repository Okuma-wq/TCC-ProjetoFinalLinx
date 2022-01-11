using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicoController : ControllerBase
    {
        private ITopicoService _topico { get; }

        public TopicoController(ITopicoService topico)
        {
            _topico = topico;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TopicoSemEntidadeDTO>> CadastrarAsync(CriarTopicoDTO novoTopico)
        {
            novoTopico.Validar();
            if (!novoTopico.IsValid)
                return BadRequest(novoTopico.Notifications);
            var resposta = await _topico.CadastrarAsync(novoTopico);

            if (resposta == null)
                return BadRequest("ReuniaoId inválido!");

            return Created("", resposta);
        }

        
        [Authorize]
        [HttpGet("{reuniaoId}")]
        public async Task<ActionResult<IEnumerable<TopicoSemEntidadeDTO>>> ListarPorReuniaoIdAsync(Guid reuniaoId)
        {
            var resposta = await _topico.ListarPorReuniaoIdAsync(reuniaoId);
            return Ok(resposta);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAsync(Guid id)
        {
            await _topico.DeletarAsync(id);
            return Ok();
        }
    }
}
