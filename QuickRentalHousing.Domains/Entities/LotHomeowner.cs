using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Domains.Entities
{
    [Index(nameof(LotId), nameof(HomeownerId), IsUnique = true)]
    public class LotHomeowner : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid LotId { get; set; }
        public Guid HomeownerId { get; set; }

        public Lot Lot { get; set; }
        public Homeowner Homeowner { get; set; }
    }
}
