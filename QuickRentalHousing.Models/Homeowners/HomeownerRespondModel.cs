using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Models.Homeowners
{
    public class HomeownerRespondModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PID { get; set; }
        public DateTime DOB { get; set; }
        public string AddressNumber { get; set; }
        public string StreetName { get; set; }
        public string DistrictName { get; set; }
        public IEnumerable<string> PhoneNumbers { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public string Description { get; set; }
    }
}
