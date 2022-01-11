using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private IAnuncioRepository _anuncioRepository { get; }
        private IMapper _mapper { get; }

        public AnuncioController(IAnuncioRepository anuncioRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Lojista")]
        [HttpPost]
        public async Task<ActionResult<AnuncioRespostaDTO>> CadastrarAsync(CriarAnuncioDTO anuncioNovo)
        {
            var lojistaId = Guid.Parse(HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value);
            anuncioNovo.Validar();
            if (!anuncioNovo.IsValid)
                return BadRequest(anuncioNovo.Notifications);

            var anuncio = _mapper.Map<Anuncio>(anuncioNovo);
            anuncio.IdUsuario = lojistaId;

            await _anuncioRepository.CadastrarAsync(anuncio);

            var anuncioResposta = _mapper.Map<AnuncioRespostaDTO>(anuncio);

            return Created("", anuncioResposta);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnuncioRespostaDTO>>> ListarAtivosAsync()
        {
            var resposta = await _anuncioRepository.ListarAtivosAsync();

            var respostaTratada = _mapper.Map<IEnumerable<AnuncioRespostaDTO>>(resposta);

            return Ok(respostaTratada);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnuncioComUsuarioDTO>> BuscarPorIdAnuncio(Guid id)
        {
            var resposta = _mapper.Map<AnuncioComUsuarioDTO>(await _anuncioRepository.BuscarPorIdAnuncioComUsuarioAsync(id));

            if (resposta == null)
                return NotFound();

            return Ok(resposta);
        }

        [HttpGet("buscar/{titulo}")]
        public async Task<ActionResult<IEnumerable<AnuncioRespostaDTO>>> BuscarPorTituloAsync(string titulo)
        {
            var resposta = await _anuncioRepository.BuscarPorTituloAsync(titulo);

            var respostaTratada = _mapper.Map<IEnumerable<AnuncioRespostaDTO>>(resposta);

            return Ok(respostaTratada);
        }

        // fazer teste de usuário que não é o dono do anuncio tentando deletar ele
        [Authorize(Roles = "Lojista")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(Guid id)
        {
            var anuncioBuscado = await _anuncioRepository.BuscarPorIdAnuncioSemUsuarioAsync(id);
            if (anuncioBuscado == null)
                return NotFound();

            var lojistaId = Guid.Parse(HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value);
            if (anuncioBuscado.IdUsuario != lojistaId)
                return Unauthorized();

            anuncioBuscado.Status = StatusAnuncioEnum.Deletado;
            await _anuncioRepository.DeletarPorId(anuncioBuscado);

            return Ok("Anuncio deletado com sucesso!");
        }
    }
}
