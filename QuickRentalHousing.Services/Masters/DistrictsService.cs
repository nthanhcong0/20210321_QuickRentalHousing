using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System.Linq;

namespace QuickRentalHousing.Services.Masters
{
    public class DistrictsService : IDistrictsService
    {
        private readonly IRepository<District> _repository;

        public DistrictsService(IRepository<District> repository)
        {
            _repository = repository;
        }

        public IQueryable<District> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }
    }

    public interface IDistrictsService
    {
        IQueryable<District> GetAllActive(bool isTracking = false);
    }
}
