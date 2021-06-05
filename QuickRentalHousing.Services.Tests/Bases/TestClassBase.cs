using ITHenry.Commons.DependencyInjection.Extensions;
using ITHenry.Commons.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickRentalHousing.Domains;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Services.Masters;
using System;
using System.IO;

namespace QuickRentalHousing.Services.Tests.Bases
{
    public class TestClassBase
    {
        private readonly IServiceProvider _serviceProvider;

        public TestClassBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(implementationFactory =>
            {
                var result = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                return result;
            });

            var assemblyNameOfDomainProject = typeof(DesignTimeDbContextFactory).Assembly.GetName().Name;

            services.AddDbContext<QuickRentalHousingDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("QuickRentalHousing.Api");

                dbContextOptionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(assemblyNameOfDomainProject));
            });
            services.AdddBaseInfrastructuresOfEntityFrameworkCoreAsScoped();
            services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>),
                ServiceLifetime.Scoped));

            services.RegisterNonGenericClassesInAssemblyAsScoped(assemblyNameOfDomainProject);

            var assemblyNameOfServiceProject = typeof(ITenantsService).Assembly.GetName().Name;
            services.RegisterNonGenericClassesInAssemblyAsScoped(assemblyNameOfServiceProject);

            _serviceProvider = services.BuildServiceProvider();
        }

        public T ResolveService<T>()
        {
            var result = this._serviceProvider.GetService<T>();

            return result;
        }
    }
}
