using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(TenantId), nameof(PhoneNumber), IsUnique = true)]
    public class TenantPhone : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
