using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Models.Homeowners
{
    public class HomeownerDetailRespondModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public HomeownerDetailRespondModel_Gender Gender { get; set; }
        public string PID { get; set; }
        public DateTime DOB { get; set; }
        public string AddressNumber { get; set; }
        public HomeownerDetailRespondModel_Street Street { get; set; }
        public HomeownerDetailRespondModel_District District { get; set; }
        public IEnumerable<HomeownerDetailRespondModel_PhoneNumber> PhoneNumbers { get; set; }
        public IEnumerable<HomeownerDetailRespondModel_Email> Emails { get; set; }
        public string Description { get; set; }
    }

    public class HomeownerDetailRespondModel_Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeownerDetailRespondModel_Street
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeownerDetailRespondModel_District
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeownerDetailRespondModel_PhoneNumber
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class HomeownerDetailRespondModel_Email
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
