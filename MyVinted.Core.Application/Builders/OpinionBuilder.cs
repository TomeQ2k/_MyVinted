using MyVinted.Core.Application.Builders.Interfaces;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.Builders
{
    public class OpinionBuilder : IOpinionBuilder
    {
        private readonly Opinion opinion = new Opinion();

        public IOpinionBuilder SetText(string text)
        {
            opinion.SetText(text);

            return this;
        }

        public IOpinionBuilder SetRating(int rating)
        {
            opinion.SetRating(rating);

            return this;
        }

        public IOpinionBuilder AboutUser(string userId)
        {
            opinion.AboutUser(userId);

            return this;
        }

        public IOpinionBuilder AddedBy(string creatorId)
        {
            opinion.AddedBy(creatorId);

            return this;
        }

        public Opinion Build() => opinion;
    }
}