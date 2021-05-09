namespace MyVinted.Core.Application.Models
{
    public record PremiumStatsModel
    (
        int OffersCount,
        int OrdersCount,
        long OrdersTotalAmount,
        int OfferLikesCount,
        int UserFollowsCount,
        int OpinionsCount
    ) : StatsModel(OffersCount, OrdersCount, OrdersTotalAmount);
}