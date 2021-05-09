using System.Threading.Tasks;

namespace MyVinted.Core.Application.Services
{
    public interface IBookingCleaner
    {
        Task<bool> ClearExpiredBookings();
    }
}