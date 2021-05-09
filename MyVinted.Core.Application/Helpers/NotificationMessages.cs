namespace MyVinted.Core.Application.Helpers
{
    public static class NotificationMessages
    {
        public static string UserFollowMessage(string username) => $"User {username} followed you";
        public static string OfferFollowMessage(string username, string offerId) => $"User {username} liked your offer #{offerId}";
        public static string NewOfferPriceProposedMessage(decimal newPrice, string offerId) => $"New offer #{offerId} price was proposed: {newPrice} $";
        public static string OfferAuctionAccepted(string offerId) => $"New price was accepted for offer #{offerId}";
        public static string OfferAuctionDenied(string offerId) => $"New price was denied for offer #{offerId}";
        public static string NewOfferByFollowedUserAddedMessage(string username) => $"New offer was added by {username}";
        public static string OfferBoughtMessage(string username, string productName) => $"User {username} bought your offer: {productName}";
        public static string NewOpinionAdded(string username) => $"User {username} added opinion about you";
    }
}