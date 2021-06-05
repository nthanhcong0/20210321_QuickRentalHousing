using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class TenantsService : ITenantsService
    {
        private readonly IRepository<Tenant> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOccupationsService _occupationsService;
        private readonly IStreetsService _streetsService;
        private readonly ITenantPhonesService _tenantPhonesService;
        private readonly ITenantEmailsService _tenantEmailsService;

        public TenantsService(IRepository<Tenant> repository,
            IUnitOfWork unitOfWork,
            IOccupationsService occupationsService,
            IStreetsService streetsService,
            ITenantPhonesService tenantPhonesService,
            ITenantEmailsService tenantEmailsService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _occupationsService = occupationsService;
            _streetsService = streetsService;
            _tenantPhonesService = tenantPhonesService;
            _tenantEmailsService = tenantEmailsService;
        }

        public async Task<Tenant> CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            Guid? occupationId,
            string occupationName,
            string addressNumber,
            Guid? streetId,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = await BuildEntityAsync(firstName, middleName, lastName,
                genderId, pid, dob, occupationId, occupationName, addressNumber,
                streetId, streetName, districtId, phoneNumbers, emails,
                description, executedBy, executedTime);

            await _repository.AddAsync(result);
            await _unitOfWork.CommitAsync();

            return result;
        }

        public async Task<dynamic> GetAsync()
        {
            var result = await GetAllActive()
                .Select(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.MiddleName,
                    x.LastName,
                    GenderName = x.Gender.Name,
                    x.PID,
                    x.DOB,
                    OccupationName = x.Occupation.Name,
                    x.AddressNumber,
                    StreetName = x.Street.Name,
                    DistrictName = x.District.Name,
                    PhoneNumbers = x.TenantPhones.Where(y => y.IsActive)
                        .Select(x => x.PhoneNumber)
                        .ToArray(),
                    Emails = x.TenantEmails.Where(y => y.IsActive)
                        .Select(x => x.Email)
                        .ToArray(),
                    x.Description,
                }).ToArrayAsync();

            return result;
        }

        public async Task<dynamic> GetAsync(Guid id)
        {
            var result = await GetActiveById(id)
                .Select(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.MiddleName,
                    x.LastName,
                    Gender = new
                    {
                        x.GenderId,
                        x.Gender.Name
                    },
                    x.PID,
                    x.DOB,
                    Occupation = new
                    {
                        x.OccupationId,
                        x.Occupation.Name,
                    },
                    x.AddressNumber,
                    Street = new
                    {
                        x.StreetId,
                    },
                    Distric = new
                    {
                        x.DistrictId,
                        x.District.Name,
                    },
                    PhoneNumbers = x.TenantPhones.Where(y => y.IsActive)
                        .Select(x => new
                        {
                            x.Id,
                            x.PhoneNumber
                        }).ToArray(),
                    Emails = x.TenantEmails.Where(y => y.IsActive)
                        .Select(x => new
                        {
                            x.Id,
                            x.Email
                        }).ToArray(),
                    x.Description,
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Tenant> UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            Guid? occupationId,
            string occupationName,
            string addressNumber,
            Guid? streetId,
            string streetName,
            int disttrictId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = await GetActiveById(id, true)
                .Include(x => x.TenantPhones)
                .Include(x => x.TenantEmails)
                .FirstAsync();
            result.FirstName = firstName;
            result.MiddleName = middleName;
            result.LastName = lastName;
            result.GenderId = genderId;
            result.PID = pid;
            result.DOB = dob;
            result.AddressNumber = addressNumber;
            result.DistrictId = disttrictId;
            result.Description = description;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            var occupation = await _occupationsService.GetOrCreateAsync(occupationId,
                occupationName, null, executedBy, executedTime);
            result.OccupationId = occupation.Id;

            var street = await _streetsService.GetOrCreateAsync(streetId,
                streetName, null, executedBy, executedTime);
            result.StreetId = street.Id;

            foreach (var item in result.TenantPhones)
            {
                if (phoneNumbers != null &&
                    phoneNumbers.Contains(item.PhoneNumber))
                {
                    continue;
                }

                item.IsActive = false;
                item.UpdatedBy = executedBy;
                item.UpdatedTime = executedTime;
            }
            if (phoneNumbers != null)
            {
                foreach (var item in phoneNumbers)
                {
                    if (result.TenantPhones.Any(x => x.PhoneNumber == item))
                    {
                        continue;
                    }

                    var homeownerPhone = _tenantPhonesService.BuildEntity(result.Id,
                        item, null, executedBy, executedTime);
                    result.TenantPhones.Add(homeownerPhone);
                }
            }

            foreach (var item in result.TenantEmails)
            {
                if (emails != null &&
                    emails.Contains(item.Email))
                {
                    continue;
                }

                item.IsActive = false;
                item.UpdatedBy = executedBy;
                item.UpdatedTime = executedTime;
            }
            if (emails != null)
            {
                foreach (var item in emails)
                {
                    if (result.TenantEmails.Any(x => x.Email == item))
                    {
                        continue;
                    }

                    var homeownerEmail = _tenantEmailsService.BuildEntity(result.Id,
                        item, null, executedBy, executedTime);
                    result.TenantEmails.Add(homeownerEmail);
                }
            }

            await _unitOfWork.CommitAsync();

            return result;
        }

        private async Task<Tenant> BuildEntityAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            Guid? occupationId,
            string occupationName,
            string addressNumber,
            Guid? streetId,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new Tenant();
            result.FirstName = firstName;
            result.MiddleName = middleName;
            result.LastName = lastName;
            result.GenderId = genderId;
            result.PID = pid;
            result.DOB = dob;
            result.AddressNumber = addressNumber;
            result.DistrictId = districtId;
            result.Description = description;
            result.IsActive = true;
            result.CreatedBy = executedBy;
            result.CreatedTime = executedTime;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            var occupation = await _occupationsService.GetOrCreateAsync(occupationId,
                occupationName, null, executedBy, executedTime);
            result.OccupationId = occupation.Id;

            var street = await _streetsService.GetOrCreateAsync(streetId,
                streetName, null, executedBy, executedTime);
            result.StreetId = street.Id;

            result.TenantPhones = phoneNumbers?.Select(
                x => _tenantPhonesService.BuildEntity(result.Id, x, null, executedBy, executedTime))
                .ToArray();

            result.TenantEmails = emails?.Select(
                x => _tenantEmailsService.BuildEntity(result.Id, x, null, executedBy, executedTime))
                .ToArray();

            return result;
        }

        private IQueryable<Tenant> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }

        private IQueryable<Tenant> GetActiveById(Guid id,
            bool isTracking = false)
        {
            var result = GetAllActive(isTracking)
                .Where(x => x.Id == id);

            return result;
        }
    }

    public interface ITenantsService
    {
        Task<Tenant> CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            Guid? occupationId,
            string occupationName,
            string addressNumber,
            Guid? streetId,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description,
            Guid executedBy,
            DateTime executedTime);
        Task<dynamic> GetAsync();
        Task<dynamic> GetAsync(Guid id);
        Task<Tenant> UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            Guid? occupationId,
            string occupationName,
            string addressNumber,
            Guid? streetId,
            string streetName,
            int disttrictId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
