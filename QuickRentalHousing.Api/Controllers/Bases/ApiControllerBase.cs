using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace QuickRentalHousing.Api.Controllers.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected Guid ExecutedBy
        {
            get
            {
                return new Guid();
            }
        }
    }
}
