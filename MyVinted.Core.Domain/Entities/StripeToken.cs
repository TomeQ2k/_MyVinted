using System;

namespace MyVinted.Core.Domain.Entities
{
    public class StripeToken
    {
        public string Id { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;

        public virtual Order Order { get; protected set; }

        public static StripeToken Create(string tokenId) => new StripeToken { Id = tokenId };
    }
}