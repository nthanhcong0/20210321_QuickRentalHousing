using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class GendersService : IGendersService
    {
        private readonly IRepository<Gender> _repository;

        public GendersService(IRepository<Gender> repository)
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

        private IQueryable<Gender> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }
    }

    public interface IGendersService
    {
        Task<dynamic> GetSelectListAsync();
    }
}
