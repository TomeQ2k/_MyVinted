using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class BalanceAccount
    {
        public string Id { get; protected set; } = Utils.Id();
        public decimal Balance { get; protected set; }
        public string AccountId { get; protected set; }

        public virtual User Account { get; protected set; }

        public static BalanceAccount Create(string accountId, decimal balance = Constants.DefaultAccountBalance) => new BalanceAccount
        {
            Balance = balance,
            AccountId = accountId
        };

        public bool AddBalance(decimal balance) => (Balance += balance) >= 0;
    }
}
