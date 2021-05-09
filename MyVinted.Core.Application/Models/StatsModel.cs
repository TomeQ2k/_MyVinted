namespace MyVinted.Core.Application.Models
{
    public record StatsModel
    (
        int OffersCount,
        int OrdersCount,
        long OrdersTotalAmount
    );
}