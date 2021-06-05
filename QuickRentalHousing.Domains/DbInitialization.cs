using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRentalHousing.Domains
{
    public class DbInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        }

        public async Task InitializeAndSeedDataAsync()
        {
            await InitializeAsync();
            await SeedDataAsync();
        }

        private async Task InitializeAsync()
        {
            var dbContext = _serviceProvider.GetRequiredService<QuickRentalHousingDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        private async Task SeedDataAsync()
        {
            var executedBy = new Guid();
            var executedTime = DateTime.UtcNow;

            await SeedDistricsAsync(executedBy, executedTime);
            await SeedGendersAsync(executedBy, executedTime);
        }

        private async Task SeedDistricsAsync(Guid executedBy,
            DateTime executedTime)
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<District>>();
            if (await repository.AnyAsync())
            {
                return;
            }

            // Prepare data
            var preparingData = new List<string>();
            for (int i = 1; i <= 12; i++)
                preparingData.Add($"District {i:00}");
            preparingData.AddRange(new string[]
            {
                "Bình Chánh", "Bình Tân", "Bình Thạnh", "Cần Giờ",
                "Củ Chi", "Gò Vấp", "Hóc Môn", "Nhà Bè", "Phú Nhuận",
                "Tân Bình", "Tân Phú", "Thủ Đức",
            });

            // Insert data
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
            foreach (var item in preparingData)
            {
                var district = new District
                {
                    Name = item.Contains("District") ? item : $"{item} District",
                    IsActive = true,
                    CreatedBy = executedBy,
                    CreatedTime = executedTime,
                    UpdatedBy = executedBy,
                    UpdatedTime = executedTime,
                };

                await repository.AddAsync(district);
                await unitOfWork.CommitAsync();
            }
        }

        private async Task SeedGendersAsync(Guid executedBy,
            DateTime executedTime)
        {
            var repository = _serviceProvider.GetRequiredService<IRepository<Gender>>();
            if (await repository.AnyAsync())
            {
                return;
            }

            // Prepare data
            var preparingData = new string[] { "Female", "Male" };

            // Insert data
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
            foreach (var item in preparingData)
            {
                var district = new Gender
                {
                    Name = item,
                    IsActive = true,
                    CreatedBy = executedBy,
                    CreatedTime = executedTime,
                    UpdatedBy = executedBy,
                    UpdatedTime = executedTime,
                };

                await repository.AddAsync(district);
                await unitOfWork.CommitAsync();
            }
        }
    }
}
