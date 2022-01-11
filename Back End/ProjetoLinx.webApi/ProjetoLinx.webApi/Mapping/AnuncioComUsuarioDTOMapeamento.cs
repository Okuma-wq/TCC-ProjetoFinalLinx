using AutoMapper;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;

namespace ProjetoLinx.webApi.Mapping
{
    public class AnuncioComUsuarioDTOMapeamento : Profile
    {
        public AnuncioComUsuarioDTOMapeamento()
        {
            CreateMap<Anuncio, AnuncioComUsuarioDTO>()
                .ForMember(x => x.NomeAutor, u => u.MapFrom(x => x.Usuario.Nome));
        }
    }
}
