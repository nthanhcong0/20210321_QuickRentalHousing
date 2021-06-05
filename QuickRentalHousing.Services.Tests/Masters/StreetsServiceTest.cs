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
    public class StreetsServiceTest : TestClassBase
    {
        private readonly IRepository<Street> _repository;
        private readonly IStreetsService _service;

        public StreetsServiceTest()
        {
            _repository = ResolveService<IRepository<Street>>();
            _service = ResolveService<IStreetsService>();
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
            var StreetName = Guid.NewGuid().ToString();
            var isExistBefore = await _repository.AnyAsync(x => x.Name == StreetName);
            var result = await TestGetOrCreateAsync(null, StreetName);
            var isExistAfter = await _repository.AnyAsync(x => x.Name == StreetName);

            Assert.IsTrue(isExistBefore == false &&
                isExistAfter &&
                result != null &&
                result.Name == StreetName);
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
            var StreetId = Guid.NewGuid();
            var StreetName = Guid.NewGuid().ToString();
            var result = await TestGetOrCreateAsync(StreetId, StreetName);

            Assert.IsTrue(result != null &&
                result.Id != StreetId &&
                result.Name == StreetName);
        }

        [TestMethod]
        public async Task TC0401_GetOrCreateAsync_notNull_notNull_ThrowDbUpdateException()
        {
            var created = await _repository.AddAsync(new Street { Name = Guid.NewGuid().ToString() });

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => TestGetOrCreateAsync(Guid.NewGuid(), created.Name));
        }

        private async Task<Street> TestGetOrCreateAsync(Guid? id, string name)
        {
            var result = await _service.GetOrCreateAsync(id, name,
                null, Guid.NewGuid(), DateTime.UtcNow);

            return result;
        }
    }
}
