using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Services.Masters;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class DistrictsController : ApiControllerBase
    {
        private readonly IDistrictsService _districtsService;

        public DistrictsController(IDistrictsService districtsService)
        {
            _districtsService = districtsService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSelectListAsync()
        {
            var result = await _districtsService.GetSelectListAsync();

            return Ok(result);
        }
    }
}
