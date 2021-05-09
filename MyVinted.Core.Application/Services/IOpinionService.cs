using System.Threading.Tasks;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Services
{
    public interface IOpinionService
    {
        Task<Opinion> AddOpinion(string text, int rating, string userId);
        Task<bool> DeleteOpinion(string opinionId, string userId);
    }
}