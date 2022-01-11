using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class AnuncioRespostaDTO
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusAnuncioEnum Status { get; set; }

        // Composição
        public Guid IdUsuario { get; set; }
    }
}
