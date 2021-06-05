using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Api.Models.Homeowners;
using QuickRentalHousing.Services.Masters;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class HomeownersController : ApiControllerBase
    {
        private readonly IHomeownersService _homeownersService;

        public HomeownersController(IHomeownersService homeownersService)
        {
            _homeownersService = homeownersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateHomeownerModel model)
        {
            await _homeownersService.CreateAsync(model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.AddressNumber,
                model.StreetId,
                model.StreetName,
                model.DisttrictId,
                model.PhoneNumbers,
                model.Emails,
                model.Description,
                ExecutedBy,
                DateTime.UtcNow);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _homeownersService.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _homeownersService.GetAsync(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateHomeownerModel model)
        {
            var result = await _homeownersService.UpdateAsync(model.Id,
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.AddressNumber,
                model.StreetId,
                model.StreetName,
                model.DisttrictId,
                model.PhoneNumbers,
                model.Emails,
                model.Description,
                ExecutedBy,
                DateTime.UtcNow);

            return Ok(result);
        }
    }
}
