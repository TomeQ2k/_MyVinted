using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class OrderSortTypeSmartEnum : SmartEnum<OrderSortTypeSmartEnum>
    {
        protected OrderSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly OrderSortTypeSmartEnum DateDescending = new DateDescendingType();
        public static readonly OrderSortTypeSmartEnum DateAscending = new DateAscendingType();
        public static readonly OrderSortTypeSmartEnum PriceDescending = new PriceDescendingType();
        public static readonly OrderSortTypeSmartEnum PriceAscending = new PriceAscendingType();

        public abstract IQueryable<Order> Sort(IQueryable<Order> orders);

        private sealed class DateDescendingType : OrderSortTypeSmartEnum
        {
            public DateDescendingType() : base(nameof(DateDescending), (int)OrderSortType.DateDescending) { }

            public override IQueryable<Order> Sort(IQueryable<Order> orders)
                => orders.OrderByDescending(o => o.DateCreated);
        }

        private sealed class DateAscendingType : OrderSortTypeSmartEnum
        {
            public DateAscendingType() : base(nameof(DateAscending), (int)OrderSortType.DateAscending) { }

            public override IQueryable<Order> Sort(IQueryable<Order> orders)
                => orders.OrderBy(o => o.DateCreated);
        }

        private sealed class PriceDescendingType : OrderSortTypeSmartEnum
        {
            public PriceDescendingType() : base(nameof(PriceDescending), (int)OrderSortType.PriceDescending) { }

            public override IQueryable<Order> Sort(IQueryable<Order> orders)
                => orders.OrderByDescending(o => o.TotalAmount);
        }

        private sealed class PriceAscendingType : OrderSortTypeSmartEnum
        {
            public PriceAscendingType() : base(nameof(PriceAscending), (int)OrderSortType.PriceAscending) { }

            public override IQueryable<Order> Sort(IQueryable<Order> orders)
                => orders.OrderBy(o => o.TotalAmount);
        }
    }
}