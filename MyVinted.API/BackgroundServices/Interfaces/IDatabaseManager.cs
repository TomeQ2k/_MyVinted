using System.Threading.Tasks;

namespace MyVinted.API.BackgroundServices.Interfaces
{
    public interface IDatabaseManager
    {
        Task Seed();
    }
}