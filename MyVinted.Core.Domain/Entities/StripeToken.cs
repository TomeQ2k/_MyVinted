namespace MyVinted.Core.Domain.Entities
{
    public class StripeToken : BasePaymentToken
    {
        public virtual Order Order { get; protected set; }
    }
}