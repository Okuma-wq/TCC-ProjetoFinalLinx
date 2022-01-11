using Flunt.Notifications;
using Flunt.Validations;
using ProjetoLinx.webApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.DTO
{
    public class CriarAnuncioDTO : Notifiable<Notification>
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusAnuncioEnum Status { get; set; } = StatusAnuncioEnum.Inativo;
        
        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Titulo, "Titulo", "Titulo não pode ser vazio")
                    .IsNotEmpty(Descricao, "Descrição", "A descrição não pode ser deixada vazia")
            );
        }
    }

}
