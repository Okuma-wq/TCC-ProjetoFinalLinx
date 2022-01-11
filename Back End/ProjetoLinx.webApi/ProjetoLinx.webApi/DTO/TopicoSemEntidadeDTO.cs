using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class TopicoSemEntidadeDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid ReuniaoId { get; set; }
    }
}
