using Microsoft.IdentityModel.Tokens;
using ProjetoLinx.webApi.Criptografia;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains.Services
{
    public class LoginService : ILoginService
    {
        private IUsuarioRepository _usuarioRepository { get; }

        public LoginService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(Login usuarioLogin)
        {
            var usuarioBuscado = await _usuarioRepository.BuscarPorEmailAsync(usuarioLogin.Email);
            if (usuarioBuscado == null)
                return null;
            if (!Senha.ValidarHashes(usuarioLogin.Senha, usuarioBuscado.Senha))
                return null;

            return usuarioBuscado;
        }

        public string GerarToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaCodeTurSenai132"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.TipoUsuario.ToString()),
                new Claim("role", userInfo.TipoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken
                (
                    "Linx",
                    "Linx",
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
