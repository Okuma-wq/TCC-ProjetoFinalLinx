using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface ILoginService
    {
        Task<Usuario> BuscarUsuarioPorEmail(Login usuarioLogin);
        string GerarToken(Usuario usuarioInfo);
    }
}
