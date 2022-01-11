using AutoBogus;
using ProjetoLinx.webApi.Criptografia;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class UsuarioBuilder : AutoFaker<Usuario>
    {
        public UsuarioBuilder()
        {
            RuleFor(x => x.Id, faker => Guid.NewGuid());
            RuleFor(x => x.Nome, faker => faker.Person.UserName);
            RuleFor(x => x.Email, faker => faker.Person.Email);
            RuleFor(x => x.Senha, faker => faker.Internet.Password(8));
            RuleFor(x => x.Telefone, faker => "551144014747");
            RuleFor(x => x.Celular, faker => "5511940607612");
            RuleFor(x => x.Cnpj, faker => "94607053000119");
            RuleFor(x => x.RazaoSocial, faker => faker.Company.CompanyName());
            RuleFor(x => x.TipoUsuario, faker => faker.PickRandom<TipoUsuarioEnum>());
            RuleFor(x => x.DataCriacao, faker => faker.Date.Recent(7));
            RuleFor(x => x.Anuncios, faker => new List<Anuncio>());
            RuleFor(x => x.Propostas, faker => new List<PropostaParaAnuncio>());
            RuleFor(x => x.ReunioesFornecedores, faker => new List<Reuniao>());
            RuleFor(x => x.ReunioesLojistas, faker => new List<Reuniao>());
        }
    }
    public static class UsuarioBuilderExtensions
    {
        public static UsuarioBuilder ComTipoUsuarioFornecedor(this UsuarioBuilder builder)
        {
            builder.RuleFor(x => x.TipoUsuario, TipoUsuarioEnum.Fornecedor);

            return builder;
        }
        public static UsuarioBuilder ComTipoUsuarioLojista(this UsuarioBuilder builder)
        {
            builder.RuleFor(x => x.TipoUsuario, TipoUsuarioEnum.Lojista);

            return builder;
        }
    }
}
