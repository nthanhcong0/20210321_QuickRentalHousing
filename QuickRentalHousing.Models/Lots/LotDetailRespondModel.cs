using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Models.Lots
{
    public class LotDetailRespondModel
    {
        public Guid Id { get; set; }
        public decimal ServiceFee { get; set; }
        public string AddressNumber { get; set; }
        public decimal RentalFee { get; set; }
        public Single TotalArea { get; set; }
        public DateTime ExpiredTime { get; set; }
        public LotDetailRespondModel_Street Street { get; set; }
        public LotDetailRespondModel_District District { get; set; }
        public IEnumerable<LotDetailRespondModel_Homeowner> Homeowners { get; set; }
        public string Description { get; set; }
    }

    public class LotDetailRespondModel_Street
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class LotDetailRespondModel_District
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LotDetailRespondModel_Homeowner
    {
        public Guid Id { get; set; }
        public string Homeowner { get; set; }
    }
}
