using ProjetoLinx.webApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class Anexo : Base
    {
        public string Nome { get; set; }
        public string Caminho { get; set; }


        public Guid ReuniaoId { get; set; }
        public Reuniao Reuniao { get; set; }
    }
}
