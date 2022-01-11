using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLinx.webApi.Criptografia;
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
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; }
        private IMapper _mapper { get; }
        private readonly ISenha _senha;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, ISenha senha)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _senha = senha;
        }

        
        [HttpPost]
        public async Task<ActionResult> CadastrarAsync(UsuarioDTO usuarioNovo)
        {
            usuarioNovo.Validar();

            if (!usuarioNovo.IsValid)
                return BadRequest(usuarioNovo.Notifications);

            var usuario = _mapper.Map<Usuario>(usuarioNovo);

            usuario.Senha = _senha.Criptografar(usuario.Senha);

            await _usuarioRepository.CadastrarAsync(usuario);

            return Created("", usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarAsync()
        {
            return Ok(await _usuarioRepository.ListarAsync());
        }
    }
}
