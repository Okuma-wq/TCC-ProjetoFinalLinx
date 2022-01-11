using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class AnuncioComUsuarioDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusAnuncioEnum Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid IdUsuario { get; set; }

        // Usuario
        public string NomeAutor { get; set; }
    }
}
