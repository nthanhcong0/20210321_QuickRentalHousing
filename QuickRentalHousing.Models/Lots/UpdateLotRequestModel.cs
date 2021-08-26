using System;
using System.Collections.Generic;

namespace QuickRentalHousing.Models.Lots
{
	public class UpdateLotRequestModel
	{
		public Guid Id { get; set; }
		public decimal ServiceFee { get; set; }
		public string AddressNumber { get; set; }
		public decimal RentalFee { get; set; }
		public Single TotalArea { get; set; }
		public DateTime ExpiredTime { get; set; }
		public int LotStatus { get; set; }
		public string StreetName { get; set; }
		public string DistrictName { get; set; }
		public string Description { get; set; }

		public IEnumerable<string> Homeowners { get; set; }
	}
}
