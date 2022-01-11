using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Interfaces
{
    public interface IAnexoService
    {
        Task<Anexo> CadastrarAsync(CriarAnexoDTO novoAnexo);
    }
}
