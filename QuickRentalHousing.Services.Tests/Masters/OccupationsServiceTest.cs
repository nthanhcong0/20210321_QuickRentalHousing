using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Services.Masters;
using QuickRentalHousing.Services.Tests.Bases;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Tests
{
    [TestClass]
    public class OccupationsServiceTest : TestClassBase
    {
        private readonly IRepository<Occupation> _repository;
        private readonly IOccupationsService _service;

        public OccupationsServiceTest()
        {
            _repository = ResolveService<IRepository<Occupation>>();
            _service = ResolveService<IOccupationsService>();
        }

        [TestMethod]
        public async Task TC01_GetOrCreateAsync_null_null_ThrowDbUpdateException()
        {
            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => TestGetOrCreateAsync(null, null));
        }

        [TestMethod]
        public async Task TC02_GetOrCreateAsync_null_notNull_Successfully()
        {
            var occupationName = Guid.NewGuid().ToString();
            var isExistBefore = await _repository.AnyAsync(x => x.Name == occupationName);
            var result = await TestGetOrCreateAsync(null, occupationName);
            var isExistAfter = await _repository.AnyAsync(x => x.Name == occupationName);

            Assert.IsTrue(isExistBefore == false &&
                isExistAfter &&
                result != null &&
                result.Name == occupationName);
        }

        [TestMethod]
        public async Task TC03_GetOrCreateAsync_notNull_null_ThrowDbUpdateException()
        {
            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => TestGetOrCreateAsync(Guid.NewGuid(), null));
        }

        [TestMethod]
        public async Task TC04_GetOrCreateAsync_notNull_notNull_Successfully()
        {
            var occupationId = Guid.NewGuid();
            var occupationName = Guid.NewGuid().ToString();
            var result = await TestGetOrCreateAsync(occupationId, occupationName);

            Assert.IsTrue(result != null &&
                result.Id != occupationId &&
                result.Name == occupationName);
        }

        [TestMethod]
        public async Task TC0401_GetOrCreateAsync_notNull_notNull_ThrowDbUpdateException()
        {
            var created = await _repository.AddAsync(new Occupation { Name = Guid.NewGuid().ToString() });

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => TestGetOrCreateAsync(Guid.NewGuid(), created.Name));
        }

        private async Task<Occupation> TestGetOrCreateAsync(Guid? id, string name)
        {
            var result = await _service.GetOrCreateAsync(id, name,
                null, Guid.NewGuid(), DateTime.UtcNow);

            return result;
        }
    }
}
