using System;

namespace MyVinted.Core.Domain.Entities
{
    public abstract class BasePaymentToken
    {
        public string Id { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;

        public static T Create<T>(string tokenId) where T : BasePaymentToken, new() => new T
        {
            Id = tokenId
        };
    }
}