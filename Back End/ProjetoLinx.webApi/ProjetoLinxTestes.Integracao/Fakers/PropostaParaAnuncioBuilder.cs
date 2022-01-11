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
    public class PropostaParaAnuncioBuilder : AutoFaker<PropostaParaAnuncio>
    {
        public PropostaParaAnuncioBuilder(Guid fornecedorId, Guid anuncioId)
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.FornecedorId, faker => fornecedorId);
            RuleFor(x => x.AnuncioId, faker => anuncioId);
            RuleFor(x => x.Status, faker => StatusPropostaAnuncioEnum.Pendente);
            RuleFor(x => x.DataCriacao, faker => DateTime.Now);
            RuleFor(x => x.Usuario, faker => null);
            RuleFor(x => x.Anuncio, faker => null);


        }
    }
    public static class PropostaParaAnuncioBuilderExtensions
    {
        public static PropostaParaAnuncioBuilder ComUsuarioEAnuncio(this PropostaParaAnuncioBuilder builder, Usuario usuario, Anuncio anuncio)
        {
            builder.RuleFor(x => x.Usuario, () => usuario);
            builder.RuleFor(x => x.Anuncio, () => anuncio);

            return builder;
        }

        public static PropostaParaAnuncioBuilder StatusDiferenteDePendente(this PropostaParaAnuncioBuilder builder, StatusPropostaAnuncioEnum status)
        {
            builder.RuleFor(x => x.Status, () => status);

            return builder;
        }
    }
}
