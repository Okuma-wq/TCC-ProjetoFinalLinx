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
    public class UsuarioDTOBuilder : AutoFaker<Usuario>
    {
        public UsuarioDTOBuilder()
        {
            RuleFor(x => x.Nome, faker => faker.Person.UserName);
            RuleFor(x => x.Email, faker => faker.Person.Email);
            RuleFor(x => x.Senha, faker => faker.Internet.Password(8));
            RuleFor(x => x.Telefone, faker => "551144014747");
            RuleFor(x => x.Celular, faker => "5511940607612");
            RuleFor(x => x.Cnpj, faker => "94607053000119");
            RuleFor(x => x.RazaoSocial, faker => faker.Company.CompanyName());
            RuleFor(x => x.TipoUsuario, faker => faker.PickRandom<TipoUsuarioEnum>());
        }
    }
}
