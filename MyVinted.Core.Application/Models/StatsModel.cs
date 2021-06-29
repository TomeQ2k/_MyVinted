namespace MyVinted.Core.Application.Models
{
    public abstract record StatsModel
    (
        int OffersCount,
        int OrdersCount,
        long OrdersTotalAmount
    );
}