using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class HomeownersService : IHomeownersService
    {
        private readonly IRepository<Homeowner> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStreetsService _streetsService;
        private readonly IHomeownerPhonesService _homeownerPhonesService;
        private readonly IHomeownerEmailsService _homeownerEmailsService;

        public HomeownersService(IRepository<Homeowner> repository,
            IUnitOfWork unitOfWork,
            IStreetsService streetsService,
            IHomeownerPhonesService homeownerPhonesService,
            IHomeownerEmailsService homeownerEmailsService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _streetsService = streetsService;
            _homeownerPhonesService = homeownerPhonesService;
            _homeownerEmailsService = homeownerEmailsService;
        }

        public async Task<Homeowner> CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
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
                genderId, pid, dob, addressNumber, streetId, streetName, districtId,
                phoneNumbers, emails, description, executedBy, executedTime);

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
                    x.AddressNumber,
                    StreetName = x.Street.Name,
                    DistrictName = x.District.Name,
                    PhoneNumbers = x.HomeownerPhones.Where(y => y.IsActive)
                        .Select(x => x.PhoneNumber)
                        .ToArray(),
                    Emails = x.HomeownerEmails.Where(y => y.IsActive)
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
                    x.AddressNumber,
                    Street = new
                    {
                        x.StreetId,
                    },
                    District = new
                    {
                        x.DistrictId,
                        x.District.Name,
                    },
                    PhoneNumbers = x.HomeownerPhones.Where(y => y.IsActive)
                        .Select(x => new
                        {
                            x.Id,
                            x.PhoneNumber
                        }).ToArray(),
                    Emails = x.HomeownerEmails.Where(y => y.IsActive)
                        .Select(x => new
                        {
                            x.Id,
                            x.Email
                        }).ToArray(),
                    x.Description,
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Homeowner> UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
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
                .Include(x => x.HomeownerPhones)
                .Include(x => x.HomeownerEmails)
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

            var street = await _streetsService.GetOrCreateAsync(streetId,
                streetName, null, executedBy, executedTime);
            result.StreetId = street.Id;

            foreach (var item in result.HomeownerPhones)
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
                    if (result.HomeownerPhones.Any(x => x.PhoneNumber == item))
                    {
                        continue;
                    }

                    var homeownerPhone = _homeownerPhonesService.BuildEntity(result.Id,
                        item, null, executedBy, executedTime);
                    result.HomeownerPhones.Add(homeownerPhone);
                }
            }

            foreach (var item in result.HomeownerEmails)
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
                    if (result.HomeownerEmails.Any(x => x.Email == item))
                    {
                        continue;
                    }

                    var homeownerEmail = _homeownerEmailsService.BuildEntity(result.Id,
                        item, null, executedBy, executedTime);
                    result.HomeownerEmails.Add(homeownerEmail);
                }
            }

            await _unitOfWork.CommitAsync();

            return result;
        }

        private async Task<Homeowner> BuildEntityAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
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
            var result = new Homeowner();
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

            var street = await _streetsService.GetOrCreateAsync(streetId,
                streetName, null, executedBy, executedTime);
            result.StreetId = street.Id;

            result.HomeownerPhones = phoneNumbers?.Select(
                x => _homeownerPhonesService.BuildEntity(result.Id, x, null, executedBy, executedTime))
                .ToArray();

            result.HomeownerEmails = emails?.Select(
                x => _homeownerEmailsService.BuildEntity(result.Id, x, null, executedBy, executedTime))
                .ToArray();

            return result;
        }

        private IQueryable<Homeowner> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }

        private IQueryable<Homeowner> GetActiveById(Guid id,
            bool isTracking = false)
        {
            var result = GetAllActive(isTracking)
                .Where(x => x.Id == id);

            return result;
        }
    }

    public interface IHomeownersService
    {
        Task<Homeowner> CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
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
        Task<Homeowner> UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
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
