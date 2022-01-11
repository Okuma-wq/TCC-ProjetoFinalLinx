using ProjetoLinx.webApi.Common;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class Anuncio : Base
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusAnuncioEnum Status { get; set; }

        // Composição
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get;  set; }

        public IReadOnlyCollection<PropostaParaAnuncio> Propostas { get { return _propostas.ToArray(); } }
        private List<PropostaParaAnuncio> _propostas = new List<PropostaParaAnuncio>();
    }
}
