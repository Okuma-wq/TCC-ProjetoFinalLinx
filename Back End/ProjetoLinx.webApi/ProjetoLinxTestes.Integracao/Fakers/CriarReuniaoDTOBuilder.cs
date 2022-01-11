using AutoBogus;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Fakers
{
    public class CriarReuniaoDTOBuilder : AutoFaker<CriarReuniaoDTO>
    {
        public CriarReuniaoDTOBuilder(Guid fornecedorId, Guid lojistaId)
        {
            RuleFor(x => x.FornecedorId, faker => fornecedorId);
            RuleFor(x => x.LojistaId, faker => lojistaId);
        }
    }
}
