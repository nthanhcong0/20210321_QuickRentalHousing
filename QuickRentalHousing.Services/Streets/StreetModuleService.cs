using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Models.Streets;
using QuickRentalHousing.Services.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Streets
{
    public class StreetModuleService : IStreetModuleService
    {
        private readonly IStreetsService _StreetsService;

        public StreetModuleService(
            IStreetsService StreetsService)
        {
            _StreetsService = StreetsService;
        }

        public async Task<IEnumerable<StreetSelectionRespondModel>> GetSelectionModelsAsync()
        {
            var result = await _StreetsService.GetAllActive()
                .Select(x => new StreetSelectionRespondModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArrayAsync();

            return result;
        }
    }

    public interface IStreetModuleService
    {
        Task<IEnumerable<StreetSelectionRespondModel>> GetSelectionModelsAsync();
    }
}
