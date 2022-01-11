using ProjetoLinx.webApi.Models;
using System;
using System.Threading.Tasks;

namespace ProjetoLinx.webApi.Services
{
    public interface IMailService
    {
        // to send an email manually 
        Task SendEmailAsync(MailRequest mailRequest);

        // to send an email automatically with template
        Task SendAlertEmail(string emailUser, Guid id, string titulo, string nome, string razaoSocial);
    }
}
