
using AutoBogus;
using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class TopicoBuilder : AutoFaker<Topico>
    {
        public TopicoBuilder(Guid reuniaoId)
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.Titulo, faker => faker.Lorem.Sentence(3));
            RuleFor(x => x.ReuniaoId, f => reuniaoId);
            RuleFor(x => x.DataCriacao, f => DateTime.Now);
            RuleFor(x => x.Reuniao, f => null);
        }
    }
}
