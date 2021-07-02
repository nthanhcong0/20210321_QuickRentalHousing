using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Services.Districts;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class DistrictModuleController : ApiControllerBase
    {
        private readonly IDistrictModuleService _districtModuleService;

        public DistrictModuleController(IDistrictModuleService districtModuleService)
        {
            _districtModuleService = districtModuleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSelectionModelsAsync()
        {
            var result = await _districtModuleService.GetSelectionModelsAsync();

            return Ok(result);
        }
    }
}
