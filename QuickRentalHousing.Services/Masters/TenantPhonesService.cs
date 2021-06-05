using QuickRentalHousing.Domains.Entities.Masters;
using System;

namespace QuickRentalHousing.Services.Masters
{
    public class TenantPhonesService : ITenantPhonesService
    {
        public TenantPhone BuildEntity(Guid tenantId,
            string phoneNumber,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new TenantPhone();
            result.TenantId = tenantId;
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

    public interface ITenantPhonesService
    {
        TenantPhone BuildEntity(Guid tenantId,
            string phoneNumber,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
