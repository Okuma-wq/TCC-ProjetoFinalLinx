using AutoMapper;
using FluentAssertions;
using ProjetoLinx.webApi.Domains;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using ProjetoLinxTestes.Integracao.Fakers;
using ProjetoLinxTestes.Integracao.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoLinxTestes.Integracao.Controllers
{
    public class TopicoControllerTeste : TestBase
    {
        public ILoginService _loginService;
        public IMapper _mapper;
        public ITopicoRepository _topico;

        public TopicoControllerTeste(ApiWebApplicationFactory factory) : base(factory)
        {
            _loginService = (ILoginService)Scope.ServiceProvider.GetService(typeof(ILoginService));
            _mapper = (IMapper)Scope.ServiceProvider.GetService(typeof(IMapper));
            _topico = (ITopicoRepository)Scope.ServiceProvider.GetService(typeof(ITopicoRepository));
        }

        private async Task<(Reuniao, Usuario, Usuario)> CriarReuniaoNoBanco()
        {
            var fornecedor = new UsuarioBuilder().ComTipoUsuarioFornecedor().Generate();
            var lojista = new UsuarioBuilder().ComTipoUsuarioLojista().Generate();
            await Context.Usuarios.AddAsync(fornecedor);
            await Context.Usuarios.AddAsync(lojista);
            await Context.SaveChangesAsync();

            var reuniao = new ReuniaoBuilder(fornecedor.Id, lojista.Id).Generate();

            Context.ChangeTracker.Clear();
            await Context.Reunioes.AddAsync(reuniao);
            await Context.SaveChangesAsync();
            return (reuniao,fornecedor,lojista);
        }

        private async Task<IEnumerable<Topico>> CriarTopicoNoBanco(Guid reuniaoId)
        {
            var topicoCriado = new TopicoBuilder(reuniaoId).Generate(3);

            await Context.Topicos.AddRangeAsync(topicoCriado);
            await Context.SaveChangesAsync();
            return topicoCriado;
        }

        #region Cadastrar

        [Fact]
        public async Task CadastrarDeveRetornar201Created()
        {
            var (reuniao,fornecedor,lojista) = await CriarReuniaoNoBanco();
            var topicosCriados = new CriarTopicoDTOBuilder(reuniao.Id).Generate();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Topico", topicosCriados);

            resposta.Should()
                .Be201Created();
        }

        [Fact]
        public async Task CadastrarDeveRetornarOMesmoTopicoQueFoiCadastradoNoBanco()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            var topicoCriado = new CriarTopicoDTOBuilder(reuniao.Id).Generate();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Topico", topicoCriado);

            var topicoCadastrado = await resposta.Content.ReadFromJsonAsync<TopicoSemEntidadeDTO>();
            var topicoNoBanco = Mapper.Map<TopicoSemEntidadeDTO>(Context.Topicos.FirstOrDefault(x => x.Id == topicoCadastrado.Id));

            topicoCadastrado.Should()
                .BeEquivalentTo(topicoNoBanco);
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestSeCriarTopicoDTONaoForPreenchidoCorretamente()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();

            var topico = new CriarTopicoDTO();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Topico", topico);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestSeReuniaoIdNaoCorresponderANenhumaReuniao()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            var topicoCriado = new CriarTopicoDTOBuilder(reuniao.Id).Generate();
            topicoCriado.ReuniaoId = Guid.NewGuid();

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.PostAsJsonAsync("Topico", topicoCriado);

            resposta.Should()
                .Be400BadRequest();
        }

        #endregion

        #region Listar

        [Fact]
        public async Task ListarDeveRetornar200Ok()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            await CriarTopicoNoBanco(reuniao.Id);


            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetAsync($"Topico/{reuniao.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task ListarRetornarTopicosSalvosNoBanco()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            var topicosCriados = _mapper.Map<IEnumerable<TopicoSemEntidadeDTO>>(await CriarTopicoNoBanco(reuniao.Id));

            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.GetFromJsonAsync<IEnumerable<TopicoSemEntidadeDTO>>($"Topico/{reuniao.Id}");

            resposta.Should()
                .BeEquivalentTo(topicosCriados);
        }

        #endregion

        #region Deletar


        [Fact]
        public async Task DeletarAsyncDeveRetornar200Ok()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            var topicosCriados = await CriarTopicoNoBanco(reuniao.Id);
            var topicoDeletado = topicosCriados.ToList()[0];


            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.DeleteAsync($"Topico/{topicoDeletado.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task DeletarAsyncDeveRemoverOTopicoDoBanco()
        {
            var (reuniao, fornecedor, lojista) = await CriarReuniaoNoBanco();
            var topicosCriados = await CriarTopicoNoBanco(reuniao.Id);
            var topicoDeletado = topicosCriados.ToList()[0];


            var token = _loginService.GerarToken(fornecedor);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var resposta = await Client.DeleteAsync($"Topico/{topicoDeletado.Id}");

            var topicoBuscado = await _topico.BuscarPorId(topicoDeletado.Id);

            topicoBuscado.Should()
                .BeNull();
        }

        #endregion
    }
}
