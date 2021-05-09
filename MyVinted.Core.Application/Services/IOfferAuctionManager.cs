using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IOfferAuctionManager
    {
        Task<OfferAuction> CreateAuction(decimal newPrice, string offerId);

        Task<bool> AcceptAuction(string auctionId, string offerId);
        Task<bool> DenyAuction(string auctionId, string offerId);
    }
}