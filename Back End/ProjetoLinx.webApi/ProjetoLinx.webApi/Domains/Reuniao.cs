using ProjetoLinx.webApi.Common;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class Reuniao : Base
    {
        public DateTime DataReuniao { get; set; }
        public string Local { get; set; } = string.Empty;
        public StatusReuniaoEnum Status { get; set; } = StatusReuniaoEnum.Ativo;
        public string TituloAnuncio { get; set; }


        // Composição
        public Guid LojistaId { get; set; }
        public Usuario Lojista { get; set; }

        public Guid? FornecedorId { get; set; }
        public Usuario Fornecedor { get; set; }

        public IReadOnlyCollection<Topico> Topicos { get { return _topicos.ToArray(); } }
        private List<Topico> _topicos = new List<Topico>();

        public IReadOnlyCollection<Anexo> Anexos{ get { return _anexos.ToArray(); } }
        private List<Anexo> _anexos = new List<Anexo>();
    }
}
