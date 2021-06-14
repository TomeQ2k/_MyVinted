using MyVinted.Core.Application.Services.ReadOnly;
using System.Threading.Tasks;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IMessenger : IReadOnlyMessenger
    {
        Task<Message> SendMessage(string text, string recipientId);

        Task<bool> DeleteMessage(string messageId);

        Task<LikeMessageResult> LikeMessage(string messageId);

        Task<bool> ReadMessage(string messageId);
    }
}