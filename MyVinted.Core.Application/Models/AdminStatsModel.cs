namespace MyVinted.Core.Application.Models
{
    public record AdminStatsModel
    (
        int OffersCount,
        int OrdersCount,
        long OrdersTotalAmount,
        int AccountsCount,
        double AverageOffersCountPerPerson,
        double AverageUserOpinion
    ) : StatsModel(OffersCount, OrdersCount, OrdersTotalAmount);
}