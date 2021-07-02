using Microsoft.AspNetCore.Mvc;
using QuickRentalHousing.Api.Controllers.Bases;
using QuickRentalHousing.Models.Tenants;
using QuickRentalHousing.Services.Masters;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Api.Controllers
{
    public class TenantsController : ApiControllerBase
    {
        private readonly ITenantsService _tenantsService;

        public TenantsController(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTenantRequestModel model)
        {
            await _tenantsService.CreateAsync(model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.OccupationId,
                model.OccupationName,
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
            var result = await _tenantsService.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _tenantsService.GetAsync(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateTenantRequestModel model)
        {
            var result = await _tenantsService.UpdateAsync(model.Id,
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.GenderId,
                model.PID,
                model.DOB,
                model.OccupationId,
                model.OccupationName,
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
