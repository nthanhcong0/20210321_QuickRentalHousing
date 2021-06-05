using QuickRentalHousing.Domains.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickRentalHousing.Domains.Entities
{
    public class Rental03Contract : BaseEntity
    {
        [Key]
        public Guid Rental02AppointmentId { get; set; }
        public decimal ExtraRentalFee { get; set; }
        public DateTime ContractExpiredTime { get; set; }
        public string Description { get; set; }

        [ForeignKey("Rental02AppointmentId")]
        public Rental02Appointment Rental02Appointment { get; set; }
    }
}
