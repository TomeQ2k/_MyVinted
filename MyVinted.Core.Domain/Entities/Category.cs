using System.Collections.Generic;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public class Category
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Name { get; init; }

        public virtual ICollection<Offer> Offers { get; protected set; } = new HashSet<Offer>();
    }
}