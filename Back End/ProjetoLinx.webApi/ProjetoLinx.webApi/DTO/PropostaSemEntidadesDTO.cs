using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class PropostaSemEntidadesDTO
    {
        public Guid Id { get; set; }
        public Guid AnuncioId { get; set; }
        public Guid FornecedorId { get; set; }
        public StatusPropostaAnuncioEnum Status { get; set; }
    }
}
