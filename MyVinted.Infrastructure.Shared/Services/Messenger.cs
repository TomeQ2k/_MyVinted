using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVinted.Core.Application.Features.Requests.Queries.Params;
using MyVinted.Core.Application.Models;
using MyVinted.Core.Domain.Entities;
using MyVinted.Core.Domain.Data.Models;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class Messenger : IMessenger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReadOnlyAccountManager accountManager;
        private readonly IHttpContextReader httpContextReader;

        public Messenger(IUnitOfWork unitOfWork, IReadOnlyAccountManager accountManager, IHttpContextReader httpContextReader)
        {
            this.unitOfWork = unitOfWork;
            this.accountManager = accountManager;
            this.httpContextReader = httpContextReader;
        }

        public async Task<PagedList<Conversation>> GetConversations(MessengerFiltersParams filters)
        {
            var currentUser = await accountManager.GetCurrentUser();

            var conversations = currentUser.MessagesSent.Concat(currentUser.MessagesReceived)
                .OrderByDescending(m => m.DateSent)
                .GroupBy(m => new { m.SenderId, m.RecipientId })
                .Select(g =>
                {
                    var (message, isCurrentUserRecipient) = (g.First(), g.First().RecipientId == currentUser.Id);

                    var conversation = new Conversation
                    (
                        new LastMessage(message.SenderId, message.Sender.UserName, message.Text, message.DateSent, message.IsRead, message.SenderId == currentUser.Id),
                        isCurrentUserRecipient ? message.SenderId : message.RecipientId,
                        isCurrentUserRecipient ? message.Sender.UserName : message.Recipient.UserName,
                        isCurrentUserRecipient ? message.Sender.AvatarUrl : message.Recipient.AvatarUrl
                    );

                    return conversation;
                })
                .GroupBy(c => c.RecipientId)
                .Distinct()
                .Select(g => g.First());

            if (!string.IsNullOrEmpty(filters.Username))
                conversations = conversations.Where(c => c.RecipientName.ToLower().Contains(filters.Username.ToLower()));

            return conversations.ToPagedList<Conversation>(filters.PageNumber, filters.PageSize);
        }

        public async Task<IPagedList<Message>> GetMessagesThread(string recipientId, MessengerFiltersParams filters)
        {
            var currentUser = await accountManager.GetCurrentUser();

            if (currentUser.Id == recipientId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            await ReadConversation(currentUser.MessagesReceived, recipientId);

            return await unitOfWork.MessageRepository.GetMessagesThread(currentUser.Id, recipientId, (filters.PageNumber, filters.PageSize));
        }

        public async Task<Message> SendMessage(string text, string recipientId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var recipient = await unitOfWork.UserRepository.Get(recipientId) ?? throw new EntityNotFoundException("Recipient not found");

            if (currentUser.Id == recipientId)
                throw new NoPermissionsException(ErrorMessages.NotAllowedMessage);

            var message = Message.Create(text);

            currentUser.MessagesSent.Add(message);
            recipient.MessagesReceived.Add(message);

            return await unitOfWork.Complete() ? message : throw new ServerException("Sending message failed");
        }

        public async Task<bool> DeleteMessage(string messageId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var message = currentUser.MessagesSent.FirstOrDefault(m => m.Id == messageId) ?? throw new EntityNotFoundException("Message not found");

            unitOfWork.MessageRepository.Delete(message);

            return await unitOfWork.Complete();
        }

        public async Task<bool> LikeMessage(string messageId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var message = currentUser.MessagesReceived.FirstOrDefault(m => m.Id == messageId) ?? throw new EntityNotFoundException("Message not found");

            message.ToggleIsLiked();

            return await unitOfWork.Complete();
        }

        public async Task<bool> ReadMessage(string messageId)
        {
            var currentUser = await accountManager.GetCurrentUser();
            var message = currentUser.MessagesReceived.FirstOrDefault(m => m.Id == messageId) ?? throw new EntityNotFoundException("Message not found");

            message.MarkAsRead();

            return await unitOfWork.Complete();
        }

        public async Task<int> CountUnreadMessages()
            => await unitOfWork.MessageRepository.CountUnreadMessages(httpContextReader.CurrentUserId);

        #region private

        private async Task ReadConversation(IEnumerable<Message> messages, string recipientId)
        {
            var messagesToRead = messages.Where(m => !m.IsRead && m.SenderId == recipientId);

            messagesToRead.ToList().ForEach(m => m.MarkAsRead());

            unitOfWork.MessageRepository.UpdateRange(messagesToRead);

            await unitOfWork.Complete();
        }

        #endregion
    }
}