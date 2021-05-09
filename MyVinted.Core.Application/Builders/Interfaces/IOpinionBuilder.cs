using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders.Interfaces
{
    public interface IOpinionBuilder : IBuilder<Opinion>
    {
        IOpinionBuilder SetText(string text);
        IOpinionBuilder SetRating(int rating);
        IOpinionBuilder AboutUser(string userId);
        IOpinionBuilder AddedBy(string creatorId);
    }
}