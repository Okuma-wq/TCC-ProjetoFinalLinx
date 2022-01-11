using ProjetoLinx.webApi.Common;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains
{
    public class Usuario : Base
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }

        // Composição
        public IReadOnlyCollection<Anuncio> Anuncios { get { return _anuncios.ToArray(); } }
        private List<Anuncio> _anuncios = new List<Anuncio>();

        public IReadOnlyCollection<PropostaParaAnuncio> Propostas { get { return _propostas.ToArray(); } }
        private List<PropostaParaAnuncio> _propostas = new List<PropostaParaAnuncio>();

        public IReadOnlyCollection<Reuniao> ReunioesLojistas { get { return _reuniaoLojista.ToArray(); } }
        private List<Reuniao> _reuniaoLojista = new List<Reuniao>();

        public IReadOnlyCollection<Reuniao> ReunioesFornecedores{ get { return _reuniaoFornecedor.ToArray(); } }
        private List<Reuniao> _reuniaoFornecedor= new List<Reuniao>();
    }
}
