using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class CriarAnexoDTO : Notifiable<Notification>
    {
        public Guid ReuniaoId { get; set; } = Guid.Empty;
        public IFormFile Anexo { get; set; }

        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(ReuniaoId, "ReuniaoId", "O id da reunião não pode estar vazio")
                    .IsNotNull(Anexo, "Anexo", "O anexo não pode ser nulo")
            );
        }
    }
}
