using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Models.Homeowners;
using QuickRentalHousing.Services.Homeowners;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class HomeownersController : ApiControllerBase
    {
        private readonly IHomeownerModuleService _homeownerModuleService;

        public HomeownersController(IHomeownerModuleService homeownerModuleService)
        {
            _homeownerModuleService = homeownerModuleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateHomeownerRequestModel model)
        {
            await _homeownerModuleService.CreateAsync(model,
                ExecutedBy,
                DateTime.UtcNow);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _homeownerModuleService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _homeownerModuleService.GetAsync(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateHomeownerRequestModel model)
        {
            var result = await _homeownerModuleService.UpdateAsync(model,
                ExecutedBy,
                DateTime.UtcNow);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _homeownerModuleService.DeleteAsync(id,
                ExecutedBy,
                DateTime.UtcNow);

            return Ok();
        }
    }
}
