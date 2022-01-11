using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoLinx.webApi.Criptografia;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService { get; }

        public LoginController(ILoginService login)
        {
            _loginService = login;
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(Login usuarioLogin)
        {
            var usuarioBuscado = await _loginService.BuscarUsuarioPorEmail(usuarioLogin);

            if (usuarioBuscado == null)
                return StatusCode(401, "Email ou Senha incorretos");

            var token = _loginService.GerarToken(usuarioBuscado);

            return Ok(token);
        }
    }
}
