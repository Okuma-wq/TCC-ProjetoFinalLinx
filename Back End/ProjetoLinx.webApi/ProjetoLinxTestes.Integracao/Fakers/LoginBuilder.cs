using AutoBogus;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class LoginBuilder : AutoFaker<Login>
    {
        public LoginBuilder(string email, string senha)
        {
            RuleFor(x => x.Email, faker => email);
            RuleFor(x => x.Senha, faker => senha);
        }
    }
}
