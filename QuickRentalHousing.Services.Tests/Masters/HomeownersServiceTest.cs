using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Services.Masters;
using QuickRentalHousing.Services.Tests.Bases;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Tests.Masters
{
    [TestClass]
    public class HomeownersServiceTest : TestClassBase
    {
        private readonly IRepository<Homeowner> _repository;
        private readonly IHomeownersService _service;

        public HomeownersServiceTest()
        {
            _repository = ResolveService<IRepository<Homeowner>>();
            _service = ResolveService<IHomeownersService>();
        }

        [TestMethod]
        public async Task TC01_CreateAsync_ExistPID_ThrowDbUpdateException()
        {
            var executedBy = Guid.NewGuid();
            var executedTime = DateTime.UtcNow;
            var pid = Guid.NewGuid().ToString();
            await _repository.AddAsync(new Homeowner
            {
                PID = pid,
            });

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => _service.CreateAsync(null, null, null, 1, pid, DateTime.UtcNow,
                null, null, "TestStreet", 1, null, null, null, executedBy, executedTime));
        }
    }
}
