using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Base
{
    [Index(nameof(PID), IsUnique = true)]
    public class BasePersonEntity : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        [Required]
        public string PID { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        public Guid StreetId { get; set; }
        public int DistrictId { get; set; }
        public string Description { get; set; }

        public Gender Gender { get; set; }
        public Street Street { get; set; }
        public District District { get; set; }
    }
}
