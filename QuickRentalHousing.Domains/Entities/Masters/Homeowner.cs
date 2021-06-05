using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Base;
using System.Collections.Generic;

namespace QuickRentalHousing.Domains.Entities.Masters
{
    [Index(nameof(PID), IsUnique = true)]
    public class Homeowner : BasePersonEntity
    {
        public ICollection<HomeownerPhone> HomeownerPhones { get; set; }
        public ICollection<HomeownerEmail> HomeownerEmails { get; set; }
    }
}
