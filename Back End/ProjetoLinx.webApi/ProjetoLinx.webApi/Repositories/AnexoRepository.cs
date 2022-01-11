using ProjetoLinx.webApi.Data;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Repositories
{
    public class AnexoRepository : IAnexoRepository
    {
        //Injeção de dependência
        private readonly ProjetoLinxContext _context;

        public AnexoRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }

        public async Task CadastrarAsync(Anexo anexo)
        {
            await _context.Anexos.AddAsync(anexo);
            await _context.SaveChangesAsync();
        }
    }
}
