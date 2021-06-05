using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class StreetsService : IStreetsService
    {
        private readonly IRepository<Street> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StreetsService(IRepository<Street> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Street> GetOrCreateAsync(Guid? id,
            string name,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            Street result;
            if (id.HasValue)
            {
                result = await GetActiveById(id.Value)
                    .FirstOrDefaultAsync();
                if (result != null)
                {
                    return result;
                }
            }

            result = await GetActiveByName(name)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                return result;
            }

            result = await CreateAsync(name, description,
                executedBy, executedTime);

            return result;
        }

        private async Task<Street> CreateAsync(string name,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = BuildEntity(name, description,
                executedBy, executedTime);

            await _repository.AddAsync(result);
            await _unitOfWork.CommitAsync();

            return result;
        }

        private IQueryable<Street> GetActiveById(Guid id,
            bool isTracking = false)
        {
            var result = GetAllActive(isTracking)
                .Where(x => x.Id == id);

            return result;
        }

        private IQueryable<Street> GetActiveByName(string name,
            bool isTracking = false)
        {
            var result = GetAllActive(isTracking)
                .Where(x => x.Name == name);

            return result;
        }

        private IQueryable<Street> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }

        private Street BuildEntity(string name,
            string description,
            Guid executedBy,
            DateTime executedTime)
        {
            var result = new Street();
            result.Name = name;
            result.Description = description;
            result.IsActive = true;
            result.CreatedBy = executedBy;
            result.CreatedTime = executedTime;
            result.UpdatedBy = executedBy;
            result.UpdatedTime = executedTime;

            return result;
        }
    }

    public interface IStreetsService
    {
        Task<Street> GetOrCreateAsync(Guid? id,
            string name,
            string description,
            Guid executedBy,
            DateTime executedTime);
    }
}
