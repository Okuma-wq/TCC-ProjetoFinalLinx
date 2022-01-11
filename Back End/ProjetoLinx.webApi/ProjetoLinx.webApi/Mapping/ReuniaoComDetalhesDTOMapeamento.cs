using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Mapping
{
    public class ReuniaoComDetalhesDTOMapeamento : Profile
    {
        public ReuniaoComDetalhesDTOMapeamento()
        {
            CreateMap<Reuniao, ReuniaoComDetalhesDTO>()
                .ForMember(x => x.UsuarioId, u => u.MapFrom(r => r.Fornecedor == null ? r.LojistaId : r.FornecedorId))
                .ForMember(x => x.Nome, u => u.MapFrom(r => r.Fornecedor == null ? r.Lojista.Nome : r.Fornecedor.Nome))
                .ForMember(x => x.RazaoSocial, u => u.MapFrom(r => r.Fornecedor == null ? r.Lojista.RazaoSocial : r.Fornecedor.RazaoSocial));

        }
    }
}
