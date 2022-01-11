using Microsoft.EntityFrameworkCore;
using ProjetoLinx.webApi.Data;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Repositories
{
    public class TopicoRepository : ITopicoRepository
    {
        private readonly ProjetoLinxContext _context;

        public TopicoRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }

        public async Task CadastrarAsync(Topico novaTopico)
        {
            await _context.Topicos.AddAsync(novaTopico);
            await _context.SaveChangesAsync();
        }

        public async Task<Topico> BuscarPorId(Guid id)
        {
            return await _context.Topicos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeletarAsync(Topico topico)
        {
            _context.Remove(topico);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Topico>> ListaTopicosDaReuniaoAsync(Guid reuniaoId)
        {
            return await _context.Topicos
                .AsNoTracking()
                .Where(t => t.ReuniaoId == reuniaoId)
                .ToListAsync();
        }
    }
}
