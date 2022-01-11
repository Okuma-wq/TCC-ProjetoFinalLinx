using AutoBogus;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class AnuncioBuilder : AutoFaker<Anuncio>
    {
        public AnuncioBuilder()
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.IdUsuario, faker => Guid.NewGuid());
            RuleFor(x => x.Titulo, faker => faker.Commerce.ProductName());
            RuleFor(x => x.Descricao, faker => faker.Commerce.ProductDescription());
            RuleFor(x => x.Status, faker => faker.PickRandom<StatusAnuncioEnum>());
            RuleFor(x => x.DataCriacao, faker => faker.Date.Past(7));
        }
    }

    public static class AnuncioBuilderExtensions
    {
        public static AnuncioBuilder ComUsuario(this AnuncioBuilder builder, Usuario usuario)
        {
            builder.RuleFor(x => x.IdUsuario, () => usuario.Id);
            builder.RuleFor(x => x.Usuario, () => usuario);

            return builder;
        }

        public static AnuncioBuilder Ativo(this AnuncioBuilder builder)
        {
            builder.RuleFor(x => x.Status, f => StatusAnuncioEnum.Ativo);

            return builder;
        }
    }
}
