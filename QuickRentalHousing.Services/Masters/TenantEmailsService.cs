using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Services.Masters
{
    public class TenantEmailsService : ITenantEmailsService
    {
        public TenantEmail BuildEntity(Guid tenantId,
            string email,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new TenantEmail();
            result.TenantId = tenantId;
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

    public interface ITenantEmailsService
    {
        TenantEmail BuildEntity(Guid tenantId,
            string email,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
