using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickRentalHousing.Domains;
using QuickRentalHousing.Services.Tests.Bases;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Tests
{
    [Ignore]
    [TestClass]
    public class QuickRentalHousingDbContext_MigrationTest : TestClassBase
    {
        [TestMethod]
        public async Task TC01_DeleteDb()
        {
            var dbContext = this.ResolveService<QuickRentalHousingDbContext>();
            await dbContext.Database.EnsureDeletedAsync();
        }

        [TestMethod]
        public async Task TC02_ApplyMigration()
        {
            var dbContext = this.ResolveService<QuickRentalHousingDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        [TestMethod]
        public async Task TC03_SeedData()
        {
            var dbInitialization = new DbInitialization(ResolveService<IServiceProvider>());
            await dbInitialization.InitializeAndSeedDataAsync();
        }
    }
}
