using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Models
{
    public class AlertRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public Guid PropostaId { get; set; }
    }
}
