namespace MyVinted.Core.Application.Results
{
    public record StripePaymentResult : PaymentResult
    {
        public Stripe.Charge Charge { get; init; }

        public StripePaymentResult(Stripe.Charge charge) : base()
            => (Charge) = (charge);
    }
}