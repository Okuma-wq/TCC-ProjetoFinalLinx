using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.Domains;
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
    public class AnexoController : ControllerBase
    {
        private IAnexoService _anexo { get; }

        public AnexoController(IAnexoService anexo)
        {
            _anexo = anexo;
        }

        [HttpPost]
        public async Task<ActionResult<Anexo>> CadastrarAsync([FromForm]CriarAnexoDTO criarAnexo)
        {
            criarAnexo.Validar();
            if (!criarAnexo.IsValid)
                return BadRequest(criarAnexo.Notifications);

            var resposta = await _anexo.CadastrarAsync(criarAnexo);

            return Created("", resposta);
        }
    }
}
