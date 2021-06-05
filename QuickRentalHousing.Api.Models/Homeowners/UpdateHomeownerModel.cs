using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Api.Models.Homeowners
{
    public class UpdateHomeownerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string PID { get; set; }
        public DateTime DOB { get; set; }
        public Guid? OccupationId { get; set; }
        public string OccupationName { get; set; }
        public string AddressNumber { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }
        public int DisttrictId { get; set; }
        public IEnumerable<string> PhoneNumbers { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public string Description { get; set; }
    }
}
