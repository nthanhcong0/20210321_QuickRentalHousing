using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class DistrictsService : IDistrictsService
    {
        private readonly IRepository<District> _repository;

        public DistrictsService(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task<dynamic> GetSelectListAsync()
        {
            var result = await GetAllActive().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToArrayAsync();

            return result;
        }

        private IQueryable<District> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }
    }

    public interface IDistrictsService
    {
        Task<dynamic> GetSelectListAsync();
    }
}
