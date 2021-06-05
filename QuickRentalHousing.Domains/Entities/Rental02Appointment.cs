using QuickRentalHousing.Domains.Entities.Base;
using QuickRentalHousing.Domains.Enums;
using System;

namespace QuickRentalHousing.Domains.Entities
{
    public class Rental02Appointment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid Rental01RegistrationId { get; set; }
        public Guid LotHomeownerId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public AppointmentStatusEnum AppointmentStatus { get; set; }
        public string RejectedReason { get; set; }
        public string Description { get; set; }

        public Rental01Registration Rental01Registration { get; set; }
        public LotHomeowner LotHomeowner { get; set; }
    }
}
