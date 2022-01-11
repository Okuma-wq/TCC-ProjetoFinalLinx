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
    public class ReuniaoRepository : IReuniaoRepository
    {
        private readonly ProjetoLinxContext _context;

        public ReuniaoRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }

        public async Task CadastarAsync(Reuniao novaReuniao)
        {
            await _context.Reunioes.AddAsync(novaReuniao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reuniao>> ListarReunioesDoFornecedorAsync(Guid id)
        {
            return await _context.Reunioes
                .AsNoTracking()
                .Where(x => x.FornecedorId == id)
                .Select(r => new Reuniao()
                {
                    Id = r.Id,
                    Local = r.Local,
                    DataReuniao = r.DataReuniao,
                    DataCriacao = r.DataCriacao,
                    Status = r.Status,
                    LojistaId = r.LojistaId,
                    TituloAnuncio = r.TituloAnuncio,
                    Lojista = new Usuario
                    {
                        Nome = r.Lojista.Nome,
                        RazaoSocial = r.Lojista.RazaoSocial
                    }
                })
                .OrderByDescending(x => x.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reuniao>> ListarReunioesDoLojistaAsync(Guid id)
        {
            return await _context.Reunioes
                .AsNoTracking()
                .Where(x => x.LojistaId == id)
                .Select(r => new Reuniao()
                {
                    Id = r.Id,
                    Local = r.Local,
                    DataReuniao = r.DataReuniao,
                    DataCriacao = r.DataCriacao,
                    Status = r.Status,
                    TituloAnuncio = r.TituloAnuncio,
                    Fornecedor = new Usuario
                    {
                        Nome = r.Fornecedor.Nome,
                        RazaoSocial = r.Fornecedor.RazaoSocial
                    }
                })
                .OrderByDescending(x => x.DataCriacao)
                .ToListAsync();
        }

        public async Task<Reuniao> BuscarPorIdDaReuniaoAsync(Guid id)
        {
            return await _context.Reunioes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AlterarAsync(Reuniao reuniaoAlterada)
        {

            _context.Reunioes.Update(reuniaoAlterada);
            await _context.SaveChangesAsync();
        }

        public async Task<Reuniao> BuscarPorIdDaReuniaoDoLojistaAsync(Guid id)
        {
            return await _context.Reunioes
                .AsNoTracking()
                .Select(r => new Reuniao()
                {
                    Id = r.Id,
                    Local = r.Local,
                    DataReuniao = r.DataReuniao,
                    Status = r.Status,
                    FornecedorId = r.FornecedorId,
                    TituloAnuncio = r.TituloAnuncio,
                    Fornecedor = new Usuario
                    {
                        Nome = r.Fornecedor.Nome,
                        RazaoSocial = r.Fornecedor.RazaoSocial
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Reuniao> BuscarPorIdDaReuniaoDoFornecedorAsync(Guid id)
        {
            return await _context.Reunioes
                .AsNoTracking()
                .Select(r => new Reuniao()
                {
                    Id = r.Id,
                    Local = r.Local,
                    DataReuniao = r.DataReuniao,
                    Status = r.Status,
                    LojistaId = r.LojistaId,
                    TituloAnuncio = r.TituloAnuncio,
                    Lojista = new Usuario
                    {
                        Nome = r.Lojista.Nome,
                        RazaoSocial = r.Lojista.RazaoSocial
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
