using AutoBogus;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class AnuncioRespostaDTOBuilder : AutoFaker<AnuncioRespostaDTO>
    {
        public AnuncioRespostaDTOBuilder()
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.IdUsuario, faker => Guid.NewGuid());
            RuleFor(x => x.Titulo, faker => faker.Commerce.ProductName());
            RuleFor(x => x.Descricao, faker => faker.Commerce.ProductDescription());
            RuleFor(x => x.Status, faker => faker.PickRandom<StatusAnuncioEnum>());
            RuleFor(x => x.DataCriacao, faker => faker.Date.Past(7));
        }
    }

    public static class AnuncioRespostaDTOBuilderExtensions
    {
        public static AnuncioRespostaDTOBuilder ComIdUsuario(this AnuncioRespostaDTOBuilder builder, Guid id)
        {
            builder.RuleFor(x => x.IdUsuario, () => id);

            return builder;
        }
    }
}
