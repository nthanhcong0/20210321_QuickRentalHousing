using QuickRentalHousing.Domains.Entities.Base;
using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Domains.Entities
{
    public class Rental01Registration : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public decimal ServiceFee { get; set; }
        public bool IsPaid { get; set; }
        public string Description { get; set; }

        public Tenant Tenant { get; set; }
    }
}
