using Ardalis.SmartEnum;
using MyVinted.Core.Common.Enums;
using System.Linq;
using MyVinted.Core.Domain.Entities;

namespace MyVinted.Core.Application.SmartEnums
{
    public abstract class OfferSortTypeSmartEnum : SmartEnum<OfferSortTypeSmartEnum>
    {
        protected OfferSortTypeSmartEnum(string name, int value) : base(name, value) { }

        public static readonly OfferSortTypeSmartEnum VerifiedDescending = new VerifiedDescendingType();
        public static readonly OfferSortTypeSmartEnum Newest = new NewestType();
        public static readonly OfferSortTypeSmartEnum Oldest = new OldestType();
        public static readonly OfferSortTypeSmartEnum PriceAscending = new PriceAscendingType();
        public static readonly OfferSortTypeSmartEnum PriceDescending = new PriceDescendingType();

        public abstract IQueryable<Offer> Sort(IQueryable<Offer> offers);

        private sealed class VerifiedDescendingType : OfferSortTypeSmartEnum
        {
            public VerifiedDescendingType() : base(nameof(VerifiedDescending), (int)OfferSortType.VerifiedDescending) { }

            public override IQueryable<Offer> Sort(IQueryable<Offer> offers)
                => offers.OrderByDescending(o => o.IsVerified);
        }

        private sealed class NewestType : OfferSortTypeSmartEnum
        {
            public NewestType() : base(nameof(Newest), (int)OfferSortType.Newest) { }

            public override IQueryable<Offer> Sort(IQueryable<Offer> offers)
                => offers.OrderByDescending(o => o.DateCreated);
        }

        private sealed class OldestType : OfferSortTypeSmartEnum
        {
            public OldestType() : base(nameof(Oldest), (int)OfferSortType.Oldest) { }

            public override IQueryable<Offer> Sort(IQueryable<Offer> offers)
                => offers.OrderBy(o => o.DateCreated);
        }

        private sealed class PriceAscendingType : OfferSortTypeSmartEnum
        {
            public PriceAscendingType() : base(nameof(PriceAscending), (int)OfferSortType.PriceAscending) { }

            public override IQueryable<Offer> Sort(IQueryable<Offer> offers)
                => offers.OrderBy(o => o.Price);
        }

        private sealed class PriceDescendingType : OfferSortTypeSmartEnum
        {
            public PriceDescendingType() : base(nameof(PriceDescending), (int)OfferSortType.PriceDescending) { }

            public override IQueryable<Offer> Sort(IQueryable<Offer> offers)
                => offers.OrderByDescending(o => o.Price);
        }
    }
}