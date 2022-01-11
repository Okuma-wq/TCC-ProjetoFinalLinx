using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class CriarTopicoDTO : Notifiable<Notification>
    {
        public string Titulo { get; set; } = string.Empty;
        public Guid ReuniaoId { get; set; } = Guid.Empty;

        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Titulo, "TituloEmpty", "O Titulo do tópico não pode estar vazio")
                    .IsLowerOrEqualsThan(Titulo, 50, "TituloLength", "O titulo não pode exceder 50 caracteres")
                    .IsNotEmpty(ReuniaoId, "ReuniaoId", "O id da reunião não pode estar vazio")
            );
        }
    }
}
