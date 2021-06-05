using QuickRentalHousing.Domains.Entities.Base;
using QuickRentalHousing.Domains.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    public class Lot : BaseEntity
    {
        public Guid Id { get; set; }
        public decimal ServiceFee { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        public decimal RentalFee { get; set; }
        public float TotalArea { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public LotStatusEnum LotStatus { get; set; }
        public Guid StreetId { get; set; }
        public int DistrictId { get; set; }
        public string Description { get; set; }

        public Street Street { get; set; }
        public District District { get; set; }
    }
}
