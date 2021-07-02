using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Models.Districts;
using QuickRentalHousing.Services.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Districts
{
    public class DistrictModuleService : IDistrictModuleService
    {
        private readonly IDistrictsService _districtsService;

        public DistrictModuleService(
            IDistrictsService districtsService)
        {
            _districtsService = districtsService;
        }

        public async Task<IEnumerable<DistrictSelectionRespondModel>> GetSelectionModelsAsync()
        {
            var result = await _districtsService.GetAllActive()
                .Select(x => new DistrictSelectionRespondModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToArrayAsync();

            return result;
        }
    }

    public interface IDistrictModuleService
    {
        Task<IEnumerable<DistrictSelectionRespondModel>> GetSelectionModelsAsync();
    }
}
