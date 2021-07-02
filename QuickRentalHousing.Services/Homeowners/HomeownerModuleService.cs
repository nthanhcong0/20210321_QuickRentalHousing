using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Models.Homeowners;
using QuickRentalHousing.Services.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Homeowners
{
    public class HomeownerModuleService : IHomeownerModuleService
    {
        private readonly IHomeownersService _homeownersService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeownerModuleService(IHomeownersService homeownersService,
            IUnitOfWork unitOfWork)
        {
            _homeownersService = homeownersService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Homeowner> CreateAsync(CreateHomeownerRequestModel model,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = await _homeownersService.CreateAsync(
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.AddressNumber,
                model.StreetId,
                model.StreetName,
                model.DistrictId,
                model.PhoneNumbers,
                model.Emails,
                model.Description,
                executedBy,
                executedTime);

            return result;
        }

        public async Task<IEnumerable<HomeownerRespondModel>> GetAllAsync()
        {
            var result = await _homeownersService.GetAllActive()
                .Select(x => new HomeownerRespondModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    GenderName = x.Gender.Name,
                    PID = x.PID,
                    DOB = x.DOB,
                    AddressNumber = x.AddressNumber,
                    StreetName = x.Street.Name,
                    DistrictName = x.District.Name,
                    PhoneNumbers = x.HomeownerPhones.Where(y => y.IsActive)
                        .Select(x => x.PhoneNumber)
                        .ToArray(),
                    Emails = x.HomeownerEmails.Where(y => y.IsActive)
                        .Select(x => x.Email)
                        .ToArray(),
                    Description = x.Description,
                }).ToArrayAsync();

            return result;
        }

        public async Task<HomeownerDetailRespondModel> GetAsync(Guid id)
        {
            var result = await _homeownersService.GetActiveById(id)
                .Select(x => new HomeownerDetailRespondModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Gender = new HomeownerDetailRespondModel_Gender
                    {
                        Id = x.GenderId,
                        Name = x.Gender.Name,
                    },
                    PID = x.PID,
                    DOB = x.DOB,
                    AddressNumber = x.AddressNumber,
                    Street = new HomeownerDetailRespondModel_Street
                    {
                        Id = x.StreetId,
                        Name = x.Street.Name,
                    },
                    District = new HomeownerDetailRespondModel_District
                    {
                        Id = x.DistrictId,
                        Name = x.District.Name,
                    },
                    PhoneNumbers = x.HomeownerPhones.Where(y => y.IsActive)
                        .Select(x => new HomeownerDetailRespondModel_PhoneNumber
                        {
                            Id = x.Id,
                            PhoneNumber = x.PhoneNumber,
                        }).ToArray(),
                    Emails = x.HomeownerEmails.Where(y => y.IsActive)
                        .Select(x => new HomeownerDetailRespondModel_Email
                        {
                            Id = x.Id,
                            Email = x.Email,
                        }).ToArray(),
                    Description = x.Description,
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Homeowner> UpdateAsync(UpdateHomeownerRequestModel model,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = await _homeownersService.UpdateAsync(model.Id,
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.AddressNumber,
                model.StreetId,
                model.StreetName,
                model.DistrictId,
                model.PhoneNumbers,
                model.Emails,
                model.Description,
                executedBy,
                executedTime);

            return result;
        }

        public async Task DeleteAsync(Guid id,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = await _homeownersService.GetActiveById(id, true)
                .FirstOrDefaultAsync();
            result.IsActive = false;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            await _unitOfWork.CommitAsync();
        }
    }

    public interface IHomeownerModuleService
    {
        Task<Homeowner> CreateAsync(CreateHomeownerRequestModel model,
            Guid executedBy,
            DateTime executedTime);
        Task<IEnumerable<HomeownerRespondModel>> GetAllAsync();
        Task<HomeownerDetailRespondModel> GetAsync(Guid id);
        Task<Homeowner> UpdateAsync(UpdateHomeownerRequestModel model,
            Guid executedBy,
            DateTime executedTime);
        Task DeleteAsync(Guid id,
            Guid executedBy,
            DateTime executedTime);
    }
}
