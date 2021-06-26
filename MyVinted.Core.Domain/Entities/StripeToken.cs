namespace MyVinted.Core.Domain.Entities
{
    public class StripeToken : BasePaymentToken
    {
        public virtual Order Order { get; protected set; }

        public static StripeToken Create(string tokenId) => new StripeToken { Id = tokenId };
    }
}