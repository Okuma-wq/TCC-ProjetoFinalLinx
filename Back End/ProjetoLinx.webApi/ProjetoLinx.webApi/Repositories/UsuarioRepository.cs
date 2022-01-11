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
    public class UsuarioRepository : IUsuarioRepository
    {
        //Injeção de dependência
        private readonly ProjetoLinxContext _context;

        public UsuarioRepository(ProjetoLinxContext contexto)
        {
            _context = contexto;
        }


        public async Task CadastrarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> ListarAsync()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .OrderBy(x => x.DataCriacao)
                .ToListAsync();
        }

        public async Task<Usuario> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Usuario> BuscarPorIdAsync(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> BuscarPorNomeERazaoSocial(string nome, string razaoSocial)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nome == nome && u.RazaoSocial == razaoSocial);
        }
    }
}
