using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class ReuniaoComDadosParaAlterarDTO
    {
        public DateTime DataReuniao { get; set; }
        public string Local { get; set; }

        public bool ValidarDataReuniao()
        {
            return DataReuniao.Date < DateTime.Now.AddDays(7);
        }
    }
}
