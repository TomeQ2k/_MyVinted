export interface StatsModel {
  offersCount: number;
  ordersCount: number;
  ordersTotalAmount: number;
  offerLikesCount?: number;
  userFollowsCount?: number;
  opinionsCount?: number;
  accountsCount?: number;
  averageOffersCountPerPerson?: number;
  averageUserOpinion?: number;
}
