using AutoBogus;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class CriarTopicoDTOBuilder : AutoFaker<CriarTopicoDTO>
    {
        public CriarTopicoDTOBuilder(Guid reuniaoId)
        {
            RuleFor(x => x.Titulo, faker => faker.Commerce.ProductAdjective()) ;
            RuleFor(x => x.ReuniaoId, faker => reuniaoId);
        }
    }
}
