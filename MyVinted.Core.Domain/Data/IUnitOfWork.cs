using MyVinted.Core.Domain.Data.Repositories;
using System;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Domain.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IOfferRepository OfferRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Opinion> OpinionRepository { get; }
        IOfferPhotoRepository OfferPhotoRepository { get; }
        IRepository<OfferLike> OfferLikeRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IMessageRepository MessageRepository { get; }
        IRepository<OfferAuction> OfferAuctionRepository { get; }
        IOrderRepository OrderRepository { get; }
        IRepository<OrderItem> OrderItemRepository { get; }
        IRepository<Cart> CartRepository { get; }
        IRepository<StripeToken> StripeTokenRepository { get; }
        IRepository<BalanceAccount> BalanceAccountRepository { get; }
        IRepository<Connection> ConnectionRepository { get; }
        IFileRepository FileRepository { get; }

        Task<bool> Complete();
    }
}