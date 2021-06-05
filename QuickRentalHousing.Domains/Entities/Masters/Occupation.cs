using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(Name), IsUnique = true)]
    public class Occupation : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
