using FluentAssertions;
using ProjetoLinx.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Unidade.Domains
{
    public class PropostaTeste
    {
        [Fact]
        public void MetodoPropostaEstaVencidaDeveRetornarTrueSeTiveremPassadoMaisDe15DiasAposADataDeCriacao()
        {
            var proposta = new PropostaParaAnuncio();
            proposta.DataCriacao = new DateTime(2021,10,15);

            proposta.PropostaEstaVencida().Should()
                .BeTrue();
        }

        [Fact]
        public void MetodoPropostaEstaVencidaDeveRetornarFalseSeMenosDe15DiasAposADataDeCriacao()
        {
            var proposta = new PropostaParaAnuncio();
            proposta.DataCriacao = DateTime.Now;

            proposta.PropostaEstaVencida().Should()
                .BeFalse();
        }
    }
}
