using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapping
{
    public class ReuniaoComUsuarioDTOMapeamento : Profile
    {
        public ReuniaoComUsuarioDTOMapeamento()
        {
            CreateMap<Reuniao, ReuniaoComUsuarioDTO>()
                //.ForMember(x => x.UsuarioId, u => u.MapFrom(r => r.Fornecedor == null ? r.LojistaId : r.FornecedorId))
                .ForMember(x => x.Nome, u => u.MapFrom(r => r.Fornecedor == null ? r.Lojista.Nome : r.Fornecedor.Nome))
                .ForMember(x => x.RazaoSocial, u => u.MapFrom(r => r.Fornecedor == null ? r.Lojista.RazaoSocial : r.Fornecedor.RazaoSocial));
        }
      }
}
