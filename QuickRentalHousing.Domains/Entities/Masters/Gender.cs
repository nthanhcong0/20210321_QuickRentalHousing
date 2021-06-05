using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(Name), IsUnique = true)]
    public class Gender : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
