using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapping
{
    public class ReuniaoMapeamento : Profile
    {
        public ReuniaoMapeamento()
        {
            CreateMap<CriarReuniaoDTO, Reuniao>();
            CreateMap<ReuniaoComDadosParaAlterarDTO, Reuniao>();
        }
    }
}
