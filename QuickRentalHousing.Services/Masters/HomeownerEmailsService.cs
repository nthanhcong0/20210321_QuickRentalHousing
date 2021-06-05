using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Services.Masters
{
    public class HomeownerEmailsService : IHomeownerEmailsService
    {
        public HomeownerEmail BuildEntity(Guid homeownerId,
            string email,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new HomeownerEmail();
            result.HomeownerId = homeownerId;
            result.Email = email;
            result.Description = description;
            result.IsActive = true;
            result.CreatedBy = executedBy;
            result.CreatedTime = executedTime;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            return result;
        }
    }

    public interface IHomeownerEmailsService
    {
        HomeownerEmail BuildEntity(Guid homeownerId,
            string email,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
