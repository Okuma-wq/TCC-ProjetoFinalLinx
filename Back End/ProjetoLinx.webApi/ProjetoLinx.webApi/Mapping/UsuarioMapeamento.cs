using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapeamentos
{
    public class UsuarioMapeamento : Profile
    {
        public UsuarioMapeamento()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
        }
    }
}
