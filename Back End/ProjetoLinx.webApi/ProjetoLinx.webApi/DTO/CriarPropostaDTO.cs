using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class CriarPropostaDTO : Notifiable<Notification>
    {
        public Guid AnuncioId { get; set; } = Guid.Empty;
        
        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(AnuncioId, "AnuncioId", "O id do anuncio não pode estar vazio")
            );
        }
    }
}
