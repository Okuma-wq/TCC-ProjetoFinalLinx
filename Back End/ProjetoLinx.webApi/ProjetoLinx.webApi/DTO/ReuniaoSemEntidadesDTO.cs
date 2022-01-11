using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class ReuniaoSemEntidadesDTO
    {
        public DateTime DataReuniao { get; set; }
        public string Local { get; set; } = string.Empty;
        public StatusReuniaoEnum Status { get; set; } = StatusReuniaoEnum.Ativo; 
        public string TituloAnuncio { get; set; }

        
    }
}
