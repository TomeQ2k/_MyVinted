using System.Linq;
using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class OrderValidatedStatusSmartEnum : SmartEnum<OrderValidatedStatusSmartEnum>
    {
        protected OrderValidatedStatusSmartEnum(string name, int value) : base(name, value)
        {
        }

        public static OrderValidatedStatusSmartEnum All = new AllType();
        public static OrderValidatedStatusSmartEnum Validated = new ValidatedType();
        public static OrderValidatedStatusSmartEnum NotValidated = new NotValidatedType();

        public abstract IQueryable<Order> Filter(IQueryable<Order> orders);

        private sealed class AllType : OrderValidatedStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)OrderValidatedStatus.All) { }

            public override IQueryable<Order> Filter(IQueryable<Order> orders) => orders;
        }

        private sealed class ValidatedType : OrderValidatedStatusSmartEnum
        {
            public ValidatedType() : base(nameof(Validated), (int)OrderValidatedStatus.Validated) { }

            public override IQueryable<Order> Filter(IQueryable<Order> orders)
                => orders.Where(o => o.IsValidated);
        }

        private sealed class NotValidatedType : OrderValidatedStatusSmartEnum
        {
            public NotValidatedType() : base(nameof(NotValidated), (int)OrderValidatedStatus.NotValidated) { }

            public override IQueryable<Order> Filter(IQueryable<Order> orders)
                => orders.Where(o => !o.IsValidated);
        }
    }
}