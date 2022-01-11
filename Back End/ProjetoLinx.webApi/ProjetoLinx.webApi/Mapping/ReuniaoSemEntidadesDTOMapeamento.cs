using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapping
{
    public class ReuniaoSemEntidadesDTOMapeamento : Profile
    {
        public ReuniaoSemEntidadesDTOMapeamento()
        {
            CreateMap<Reuniao, ReuniaoSemEntidadesDTO>().ReverseMap();
        }
    }
}
