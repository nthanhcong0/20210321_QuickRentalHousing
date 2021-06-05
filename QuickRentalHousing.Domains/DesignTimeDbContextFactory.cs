using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace QuickRentalHousing.Domains
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<QuickRentalHousingDbContext>
    {
        public QuickRentalHousingDbContext CreateDbContext(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IConfiguration>(implementationFactory =>
            {
                var result = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "QuickRentalHousing.Api"))
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Migration.json")
                    .AddEnvironmentVariables()
                    .Build();

                return result;
            });
            services.AddDbContext<QuickRentalHousingDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("QuickRentalHousing.Api");

                dbContextOptionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(this.GetType().Namespace));
            });

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<QuickRentalHousingDbContext>();
        }
    }
}
