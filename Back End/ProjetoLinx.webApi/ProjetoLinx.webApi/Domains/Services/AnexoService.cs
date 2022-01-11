using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProjetoLinx.webApi.DTO;
using ProjetoLinx.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Domains.Services
{
    public class AnexoService : IAnexoService
    {
        private IAnexoRepository _anexo { get; }
        private IMapper _mapper { get; }

        public AnexoService(IMapper mapper, IAnexoRepository anexo)
        {
            _mapper = mapper;
            _anexo = anexo;
        }

        public (string, string) PersistirAnexoEGerarLink(IFormFile anexo)
        {
            var pathAnexos = RetornarDiretorioParaSalvarAnexos();

            var nomeAnexo = anexo.FileName.ToString();

            var tipoAnexo = nomeAnexo.Substring(nomeAnexo.LastIndexOf('.') + 1);

            var novoNomeAnexo = Guid.NewGuid();

            var caminhoCompletoDoAnexo = Path.Combine(pathAnexos, $"{novoNomeAnexo}.{tipoAnexo}");

            using (var stream = new FileStream(caminhoCompletoDoAnexo, FileMode.Create))
            {
                anexo.CopyTo(stream);
            }

            return (caminhoCompletoDoAnexo, nomeAnexo);
        }

        private string RetornarDiretorioParaSalvarAnexos()
        {
            var diretorioDaAplicacao = Directory.GetCurrentDirectory();
            var folder = Path.Combine(Directory.GetParent(diretorioDaAplicacao).ToString(), @"tempAnexos");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }

        public async Task<Anexo> CadastrarAsync(CriarAnexoDTO criarAnexo)
        {
            var (caminho, nome) = PersistirAnexoEGerarLink(criarAnexo.Anexo);

            var novoAnexo = new Anexo();
            novoAnexo.Nome = nome;
            novoAnexo.Caminho = caminho;
            novoAnexo.ReuniaoId = criarAnexo.ReuniaoId;

            await _anexo.CadastrarAsync(novoAnexo);

            return novoAnexo;
        }
    }
}
