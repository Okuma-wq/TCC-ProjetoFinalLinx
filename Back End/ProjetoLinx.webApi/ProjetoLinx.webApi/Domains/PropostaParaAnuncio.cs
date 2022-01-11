using ProjetoLinx.webApi.Common;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class PropostaParaAnuncio : Base
    {
        public Guid AnuncioId{ get; set; }
        public Guid? FornecedorId { get; set; }
        public StatusPropostaAnuncioEnum Status { get; set; } = StatusPropostaAnuncioEnum.Pendente;

        // Composição
        public Usuario Usuario { get; set; }
        public Anuncio Anuncio { get; set; }

        public bool PropostaEstaVencida()
        {
            return DateTime.Now.Subtract(DataCriacao) > TimeSpan.FromDays(15);
        }
    }
}
