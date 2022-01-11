using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class ReuniaoComUsuarioDTO
    {
        public Guid Id { get; set; }
        //public Guid UsuarioId { get; set; }
        public string Local { get; set; }
        public StatusReuniaoEnum Status { get; set; }
        public DateTime DataReuniao { get; set; }
        public string TituloAnuncio { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
    }
}
