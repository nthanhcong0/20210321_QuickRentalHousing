using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Services.Masters
{
    public class HomeownerPhonesService : IHomeownerPhonesService
    {
        public HomeownerPhone BuildEntity(Guid homeownerId,
            string phoneNumber,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new HomeownerPhone();
            result.HomeownerId = homeownerId;
            result.PhoneNumber = phoneNumber;
            result.Description = description;
            result.IsActive = true;
            result.CreatedBy = executedBy;
            result.CreatedTime = executedTime;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            return result;
        }
    }

    public interface IHomeownerPhonesService
    {
        HomeownerPhone BuildEntity(Guid homeownerId,
            string phoneNumber,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
