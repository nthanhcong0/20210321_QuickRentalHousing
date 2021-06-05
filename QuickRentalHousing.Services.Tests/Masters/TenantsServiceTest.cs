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
    public class TenantsServiceTest : TestClassBase
    {
        private readonly IRepository<Tenant> _repository;
        private readonly ITenantsService _service;

        public TenantsServiceTest()
        {
            _repository = ResolveService<IRepository<Tenant>>();
            _service = ResolveService<ITenantsService>();
        }

        [TestMethod]
        public async Task TC01_CreateAsync_ExistPID_ThrowDbUpdateException()
        {
            var executedBy = Guid.NewGuid();
            var executedTime = DateTime.UtcNow;
            var pid = Guid.NewGuid().ToString();
            await _repository.AddAsync(new Tenant
            {
                PID = pid,
            });

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => _service.CreateAsync(null, null, null, 1, pid, DateTime.UtcNow,
                null, "TestOccupation", null, null, "TestStreet", 1, null, null, null,
                executedBy, executedTime));
        }
    }
}
