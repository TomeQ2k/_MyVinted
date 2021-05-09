using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class BoughtOfferStatusSmartEnum : SmartEnum<BoughtOfferStatusSmartEnum>
    {
        protected BoughtOfferStatusSmartEnum(string name, int value) : base(name, value) { }

        public static readonly BoughtOfferStatusSmartEnum All = new AllType();
        public static readonly BoughtOfferStatusSmartEnum NotBought = new NotBoughtType();
        public static readonly BoughtOfferStatusSmartEnum Bought = new BoughtType();

        public abstract IQueryable<Offer> Filter(IQueryable<Offer> offers);

        private sealed class AllType : BoughtOfferStatusSmartEnum
        {
            public AllType() : base(nameof(All), (int)BoughtOfferStatus.All) { }

            public override IQueryable<Offer> Filter(IQueryable<Offer> offers) => offers;
        }

        private sealed class NotBoughtType : BoughtOfferStatusSmartEnum
        {
            public NotBoughtType() : base(nameof(NotBought), (int)BoughtOfferStatus.NotBought) { }

            public override IQueryable<Offer> Filter(IQueryable<Offer> offers)
                => offers.Where(o => !o.IsBought);
        }

        private sealed class BoughtType : BoughtOfferStatusSmartEnum
        {
            public BoughtType() : base(nameof(Bought), (int)BoughtOfferStatus.Bought) { }

            public override IQueryable<Offer> Filter(IQueryable<Offer> offers)
                => offers.Where(o => o.IsBought);
        }
    }
}