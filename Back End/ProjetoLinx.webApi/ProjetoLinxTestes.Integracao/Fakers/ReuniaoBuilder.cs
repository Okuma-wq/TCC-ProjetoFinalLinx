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
    public class ReuniaoBuilder : AutoFaker<Reuniao>
    {
        public ReuniaoBuilder(Guid fornecedorId, Guid lojistaId)
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.FornecedorId, faker => fornecedorId);
            RuleFor(x => x.LojistaId, faker => lojistaId);
            RuleFor(x => x.DataReuniao, faker => DateTime.Parse("0001-01-01 00:00:00.0000000"));
            RuleFor(x => x.Local, faker => "");
            RuleFor(x => x.TituloAnuncio, faker => faker.Commerce.ProductName());
            RuleFor(x => x.Status, faker => StatusReuniaoEnum.Ativo);
            RuleFor(x => x.DataCriacao, faker => DateTime.Parse("2021-11-12 16:52:53.5579759"));
            RuleFor(x => x.Fornecedor, faker => null);
            RuleFor(x => x.Lojista, faker => null);
        }
    }

    public static class ReuniaoBuilderExtensions
    {
        public static ReuniaoBuilder ComStatus(this ReuniaoBuilder builder, StatusReuniaoEnum status)
        {
            builder.RuleFor(x => x.Status, () => status);

            return builder;
        }
    }
}
