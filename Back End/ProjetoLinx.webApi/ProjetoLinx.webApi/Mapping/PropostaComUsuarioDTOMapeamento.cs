
using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapping
{
    public class PropostaComUsuarioDTOMapeamento : Profile
    {
        public PropostaComUsuarioDTOMapeamento()
        {
            CreateMap<PropostaParaAnuncio, PropostaComUsuarioDTO>()
                .ForMember(x => x.Nome, u => u.MapFrom(x => x.Usuario.Nome))
                .ForMember(x => x.RazaoSocial, u => u.MapFrom(x => x.Usuario.RazaoSocial));  
        }
    }
}
