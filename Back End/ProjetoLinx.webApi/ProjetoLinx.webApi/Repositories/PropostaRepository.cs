using Microsoft.EntityFrameworkCore;
using ProjetoLinx.webApi.Data;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.Enums;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Repositories
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly ProjetoLinxContext _context;

        public PropostaRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }

        public async Task AlterarStatusAsync(PropostaParaAnuncio propostaAlterada)
        {
            _context.Propostas.Update(propostaAlterada);
            await _context.SaveChangesAsync();
        }

        public async Task<PropostaParaAnuncio> BuscarPorPropostaIdAsync(Guid id)
        {
            return await _context.Propostas
                .AsNoTracking()
                .Include(x => x.Anuncio)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CadastrarAsync(PropostaParaAnuncio proposta)
        {
            await _context.Propostas.AddAsync(proposta);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropostaParaAnuncio>> BuscarPorAnuncioIdAsync(Guid id)
        {
            return await _context.Propostas
                .AsNoTracking()
                .Select(p => new PropostaParaAnuncio()
                {
                    Id = p.Id,
                    AnuncioId = p.AnuncioId,
                    FornecedorId = p.FornecedorId,
                    Status = p.Status,


                    Usuario = new Usuario
                    {
                        Nome = p.Usuario.Nome,
                        RazaoSocial = p.Usuario.RazaoSocial
                    }
                })
                .Where(p => p.AnuncioId == id)
                .ToListAsync();
        }

        public async Task<PropostaParaAnuncio> BuscarPorAnuncioIdEFornecedorId(Guid anuncioId, Guid fornecedorId)
        {
            return await _context.Propostas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AnuncioId == anuncioId && x.FornecedorId == fornecedorId);
        }
    }
}
