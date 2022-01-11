using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using ProjetoLinx.webApi.Enums;

namespace ProjetoLinx.webApi.DTO
{
    public class UsuarioDTO : Notifiable<Notification>
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public TipoUsuarioEnum TipoUsuario { get; set; }

        public void Validar()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsPhoneNumber(Telefone, "Telefone", "Número de telefone inválido")
                .IsNotEmpty(Nome, "Nome", "Nome não pode ser vazio")
                .IsEmail(Email, "Email", "O formato do email está incorreto")
                .IsGreaterThan(Senha, 7, "Senha", "A senha deve ter pelo menos 8 caracteres")
                // Phone number recebe em formato DDI DDD numero ex: 5511940607612 para celular e 551144014747 para telefone
                .IsPhoneNumber(Celular, "Celular", "Número de celular inválido")
                .IsCnpj(Cnpj, "Cnpj", "CNPJ inválido")
                .IsNotEmpty(RazaoSocial, "RazaoSocial", "A Razão Social não pode ser vazia")
            );
        }
    }
}
