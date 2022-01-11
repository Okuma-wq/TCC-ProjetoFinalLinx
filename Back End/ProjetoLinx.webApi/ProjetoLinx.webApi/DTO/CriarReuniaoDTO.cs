using Flunt.Notifications;
using Flunt.Validations;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class CriarReuniaoDTO : Notifiable<Notification>
    {
        // Composição
        public Guid LojistaId { get; set; } = Guid.Empty;
        public Guid FornecedorId { get; set; } = Guid.Empty;
        public string TituloAnuncio { get; set; } = string.Empty;


        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(LojistaId, "LojistaId", "O id do lojista não pode estar vazio")
                    .IsNotEmpty(FornecedorId, "FornecedorId", "O id do fornecedor não pode estar vazio")
                    .IsNotEmpty(TituloAnuncio, "TituloAnuncio", "O titulo do anuncio não pode estar vazio")
            );
        }
    }
}
