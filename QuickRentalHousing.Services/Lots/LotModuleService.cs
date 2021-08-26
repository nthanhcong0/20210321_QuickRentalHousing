using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Models.Lots;
using QuickRentalHousing.Services.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Lots
{
    public class LotModuleService : ILotModuleService
    {
        private readonly ILotsService _LotsService;
        private readonly IUnitOfWork _unitOfWork;

        public LotModuleService(ILotsService LotsService,
            IUnitOfWork unitOfWork)
        {
            _LotsService = LotsService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Lot> CreateAsync(CreateLotRequestModel model,
            Guid executedBy,
            DateTime executedTime)
        {
            //var result = await _LotsService.CreateAsync(
            //    model.FirstName,
            //    model.MiddleName,
            //    model.LastName,
            //    model.GenderId,
            //    model.PID,
            //    model.DOB,
            //    model.AddressNumber,
            //    model.StreetId,
            //    model.StreetName,
            //    model.DistrictId,
            //    model.PhoneNumbers,
            //    model.Emails,
            //    model.Description,
            //    executedBy,
            //    executedTime);

            //return result;

            return null;
        }

        public async Task<IEnumerable<LotRespondModel>> GetAllAsync()
        {
            //var result = await _LotsService.GetAllActive()
            //    .Select(x => new LotRespondModel
            //    {
            //        Id = x.Id,
            //        FirstName = x.FirstName,
            //        MiddleName = x.MiddleName,
            //        LastName = x.LastName,
            //        GenderName = x.Gender.Name,
            //        PID = x.PID,
            //        DOB = x.DOB,
            //        AddressNumber = x.AddressNumber,
            //        StreetName = x.Street.Name,
            //        DistrictName = x.District.Name,
            //        PhoneNumbers = x.LotPhones.Where(y => y.IsActive)
            //            .Select(x => x.PhoneNumber)
            //            .ToArray(),
            //        Emails = x.LotEmails.Where(y => y.IsActive)
            //            .Select(x => x.Email)
            //            .ToArray(),
            //        Description = x.Description,
            //    }).ToArrayAsync();

            //return result;

            return null;
        }

        public async Task<LotDetailRespondModel> GetAsync(Guid id)
        {
            //var result = await _LotsService.GetActiveById(id)
            //    .Select(x => new LotDetailRespondModel
            //    {
            //        Id = x.Id,
            //        FirstName = x.FirstName,
            //        MiddleName = x.MiddleName,
            //        LastName = x.LastName,
            //        Gender = new LotDetailRespondModel_Gender
            //        {
            //            Id = x.GenderId,
            //            Name = x.Gender.Name,
            //        },
            //        PID = x.PID,
            //        DOB = x.DOB,
            //        AddressNumber = x.AddressNumber,
            //        Street = new LotDetailRespondModel_Street
            //        {
            //            Id = x.StreetId,
            //            Name = x.Street.Name,
            //        },
            //        District = new LotDetailRespondModel_District
            //        {
            //            Id = x.DistrictId,
            //            Name = x.District.Name,
            //        },
            //        PhoneNumbers = x.LotPhones.Where(y => y.IsActive)
            //            .Select(x => new LotDetailRespondModel_PhoneNumber
            //            {
            //                Id = x.Id,
            //                PhoneNumber = x.PhoneNumber,
            //            }).ToArray(),
            //        Emails = x.LotEmails.Where(y => y.IsActive)
            //            .Select(x => new LotDetailRespondModel_Email
            //            {
            //                Id = x.Id,
            //                Email = x.Email,
            //            }).ToArray(),
            //        Description = x.Description,
            //    }).FirstOrDefaultAsync();

            //return result;

            return null;
        }

        public async Task<Lot> UpdateAsync(UpdateLotRequestModel model,
            Guid executedBy,
            DateTime executedTime)
        {
            //var result = await _LotsService.UpdateAsync(model.Id,
            //    model.FirstName,
            //    model.MiddleName,
            //    model.LastName,
            //    model.GenderId,
            //    model.PID,
            //    model.DOB,
            //    model.AddressNumber,
            //    model.StreetId,
            //    model.StreetName,
            //    model.DistrictId,
            //    model.PhoneNumbers,
            //    model.Emails,
            //    model.Description,
            //    executedBy,
            //    executedTime);

            //return result;

            return null;
        }

        public async Task DeleteAsync(Guid id,
            Guid executedBy,
            DateTime executedTime)
        {
            //var result = await _LotsService.GetActiveById(id, true)
            //    .FirstOrDefaultAsync();
            //result.IsActive = false;
            //result.UpdatedBy = executedBy;
            //result.UpdatedTime = executedTime;

            //await _unitOfWork.CommitAsync();
        }
    }

    public interface ILotModuleService
    {
        Task<Lot> CreateAsync(CreateLotRequestModel model,
            Guid executedBy,
            DateTime executedTime);
        Task<IEnumerable<LotRespondModel>> GetAllAsync();
        Task<LotDetailRespondModel> GetAsync(Guid id);
        Task<Lot> UpdateAsync(UpdateLotRequestModel model,
            Guid executedBy,
            DateTime executedTime);
        Task DeleteAsync(Guid id,
            Guid executedBy,
            DateTime executedTime);
    }
}
