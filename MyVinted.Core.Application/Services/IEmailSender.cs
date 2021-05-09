using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Services
{
    public interface IEmailSender
    {
        Task<bool> Send(EmailMessage emailMessage);
    }
}