using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(HomeownerId), nameof(Email), IsUnique = true)]
    public class HomeownerEmail : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid HomeownerId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
