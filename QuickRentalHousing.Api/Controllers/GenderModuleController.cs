using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Services.Genders;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class GenderModuleController : ApiControllerBase
    {
        private readonly IGenderModuleService _genderModuleService;

        public GenderModuleController(IGenderModuleService genderModuleService)
        {
            _genderModuleService = genderModuleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSelectionModelsAsync()
        {
            var result = await _genderModuleService.GetSelectionModelsAsync();

            return Ok(result);
        }
    }
}
