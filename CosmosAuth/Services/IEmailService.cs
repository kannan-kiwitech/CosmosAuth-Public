using CosmosAuth.Models.Dtos;
using System.Threading.Tasks;

namespace CosmosAuth.Services
{
    public interface IEmailService
    {
        EmailSenderOptions Options { get; }

        Task Execute(string apiKey, string subject, string message, string email);
        Task SendEmailAsync(string email, string subject, string message);
    }
}