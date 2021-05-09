using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IBookingService
    {
        Task<bool> BookOffer(string offerId);

        Task<bool> CancelBooking(Cart cart);
    }
}