using MyVinted.Core.Domain.Data;
using MyVinted.Core.Domain.Data.Repositories;
using MyVinted.Infrastructure.Persistence.Database.Repositories;
using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

#pragma warning disable 649

namespace MyVinted.Infrastructure.Persistence.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        #region repositories

        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository ?? new UserRepository(context);

        private IRepository<Role> roleRepository;
        public IRepository<Role> RoleRepository => roleRepository ?? new Repository<Role>(context);

        private IOfferRepository offerRepository;
        public IOfferRepository OfferRepository => offerRepository ?? new OfferRepository(context);

        private IRepository<Category> categoryRepository;
        public IRepository<Category> CategoryRepository => categoryRepository ?? new Repository<Category>(context);

        private IRepository<Opinion> opinionRepository;
        public IRepository<Opinion> OpinionRepository => opinionRepository ?? new Repository<Opinion>(context);

        private IOfferPhotoRepository offerPhotoRepository;
        public IOfferPhotoRepository OfferPhotoRepository => offerPhotoRepository ?? new OfferPhotoRepository(context);

        private IRepository<OfferLike> offerLikeRepository;
        public IRepository<OfferLike> OfferLikeRepository => offerLikeRepository ?? new Repository<OfferLike>(context);

        private INotificationRepository notificationRepository;

        public INotificationRepository NotificationRepository =>
            notificationRepository ?? new NotificationRepository(context);

        private IMessageRepository messageRepository;
        public IMessageRepository MessageRepository => messageRepository ?? new MessageRepository(context);

        private IRepository<OfferAuction> offerAuctionRepository;

        public IRepository<OfferAuction> OfferAuctionRepository =>
            offerAuctionRepository ?? new Repository<OfferAuction>(context);

        private IOrderRepository orderRepository;
        public IOrderRepository OrderRepository => orderRepository ?? new OrderRepository(context);

        private IRepository<OrderItem> orderItemRepository;
        public IRepository<OrderItem> OrderItemRepository => orderItemRepository ?? new Repository<OrderItem>(context);

        private IRepository<Cart> cartRepository;
        public IRepository<Cart> CartRepository => cartRepository ?? new Repository<Cart>(context);

        private IRepository<StripeToken> stripeTokenRepository;

        public IRepository<StripeToken> StripeTokenRepository =>
            stripeTokenRepository ?? new Repository<StripeToken>(context);

        private IRepository<BalanceAccount> balanceAccountRepository;

        public IRepository<BalanceAccount> BalanceAccountRepository =>
            balanceAccountRepository ?? new Repository<BalanceAccount>(context);

        private IRepository<Connection> connectionRepository;

        public IRepository<Connection> ConnectionRepository =>
            connectionRepository ?? new Repository<Connection>(context);

        private IFileRepository fileRepository;
        public IFileRepository FileRepository => fileRepository ?? new FileRepository(context);

        #endregion

        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;

        public void Dispose()
        {
            context.Dispose();
        }
    }
}