using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Models.Genders;
using QuickRentalHousing.Services.Masters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Genders
{
    public class GenderModuleService : IGenderModuleService
    {
        private readonly IGendersService _gendersService;

        public GenderModuleService(
            IGendersService gendersService)
        {
            _gendersService = gendersService;
        }

        public async Task<IEnumerable<GenderSelectionRespondModel>> GetSelectionModelsAsync()
        {
            var result = await _gendersService.GetAllActive()
                .Select(x => new GenderSelectionRespondModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToArrayAsync();

            return result;
        }
    }

    public interface IGenderModuleService
    {
        Task<IEnumerable<GenderSelectionRespondModel>> GetSelectionModelsAsync();
    }
}
