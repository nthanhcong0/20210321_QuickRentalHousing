using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Services.Masters;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class GendersController : ApiControllerBase
    {
        private readonly IGendersService _gendersService;

        public GendersController(IGendersService gendersService)
        {
            _gendersService = gendersService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSelectListAsync()
        {
            var result = await _gendersService.GetSelectListAsync();

            return Ok(result);
        }
    }
}
