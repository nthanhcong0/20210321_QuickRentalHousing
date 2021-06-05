using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(PID), IsUnique = true)]
    public class Tenant : BasePersonEntity
    {
        public Guid OccupationId { get; set; }

        public Occupation Occupation { get; set; }
        public ICollection<TenantPhone> TenantPhones { get; set; }
        public ICollection<TenantEmail> TenantEmails { get; set; }
    }
}
