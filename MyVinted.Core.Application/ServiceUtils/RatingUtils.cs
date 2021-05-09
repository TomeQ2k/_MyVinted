using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.ServiceUtils
{
    public static class RatingUtils
    {
        public static double CalculateRating(User user)
            => user.Opinions.Count != 0 ? user.Opinions.Select(o => o.Rating).Sum() / user.Opinions.Count : 0;
    }
}