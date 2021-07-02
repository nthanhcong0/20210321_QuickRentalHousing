using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System.Linq;

namespace QuickRentalHousing.Services.Masters
{
    public class GendersService : IGendersService
    {
        private readonly IRepository<Gender> _repository;

        public GendersService(IRepository<Gender> repository)
        {
            _repository = repository;
        }

        public IQueryable<Gender> GetAllActive(bool isTracking = false)
        {
            var result = _repository.GetAll(isTracking)
                .Where(x => x.IsActive);

            return result;
        }
    }

    public interface IGendersService
    {
        IQueryable<Gender> GetAllActive(bool isTracking = false);
    }
}
