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
    public class AnuncioRepository : IAnuncioRepository
    {
        //Injeção de dependência
        private readonly ProjetoLinxContext _context;

        public AnuncioRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }

        public async Task<Anuncio> BuscarPorIdAnuncioComUsuarioAsync(Guid id)
        {
            return await _context.Anuncios
                .AsNoTracking()
                .Select(a => new Anuncio()
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descricao = a.Descricao,
                    IdUsuario = a.IdUsuario,
                    DataCriacao = a.DataCriacao,
                    Status = a.Status,

                    Usuario = new Usuario
                    {
                        Nome = a.Usuario.Nome
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Anuncio>> BuscarPorTituloAsync(string titulo)
        {
            return await _context.Anuncios
                .OrderBy(x => x.DataCriacao)
                .Where(x => x.Titulo.Contains(titulo) && x.Status == Enums.StatusAnuncioEnum.Ativo)
                .ToListAsync();
        }

        public async Task CadastrarAsync(Anuncio anuncio)
        {
            await _context.Anuncios.AddAsync(anuncio);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPorId(Anuncio anuncio)
        {
            _context.Anuncios.Update(anuncio);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Anuncio>> ListarAtivosAsync()
        {
            return await _context.Anuncios
                .AsNoTracking()
                .OrderByDescending(x => x.DataCriacao)
                .Where(x => x.Status == Enums.StatusAnuncioEnum.Ativo)
                .ToListAsync();
        }

        public async Task<Anuncio> BuscarPorIdAnuncioSemUsuarioAsync(Guid id)
        {
            return await _context.Anuncios
                .AsNoTracking()
                .Where(a => a.Status != StatusAnuncioEnum.Deletado)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
