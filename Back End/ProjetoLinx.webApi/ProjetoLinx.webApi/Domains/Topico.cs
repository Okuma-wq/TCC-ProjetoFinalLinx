using ProjetoLinx.webApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class Topico : Base
    {
        public string Titulo { get; set; }

        // Composição
        public Guid ReuniaoId { get; set; }
        public Reuniao Reuniao { get; set; }

    }
}
