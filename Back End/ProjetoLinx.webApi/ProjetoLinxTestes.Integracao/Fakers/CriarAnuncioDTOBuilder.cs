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
    public class CriarAnuncioDTOBuilder : AutoFaker<CriarAnuncioDTO>
    {
        public CriarAnuncioDTOBuilder()
        {
            RuleFor(x => x.Titulo, faker => faker.Commerce.ProductName());
            RuleFor(x => x.Descricao, faker => faker.Commerce.ProductDescription());
            RuleFor(x => x.Status, faker => faker.PickRandom<StatusAnuncioEnum>());
        }
    }
}
