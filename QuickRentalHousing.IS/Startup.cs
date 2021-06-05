// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.EntityFramework.DbContexts;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickRentalHousing.IS.Data;
using System;
using System.Threading.Tasks;

namespace QuickRentalHousing.IS
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var migrationsAssembly = typeof(Startup).Assembly.FullName;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection"),
                    o => o.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlite(Configuration.GetConnectionString("ConfigurationDbConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            }).AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlite(Configuration.GetConnectionString("PersistedGrantDbConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            }).AddAspNetIdentity<ApplicationUser>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            DbInitializationAsync(app.ApplicationServices).Wait();

            DataInitializer.ExecuteAsync(app.ApplicationServices).Wait();
        }

        private async Task DbInitializationAsync(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var scopeServiceProvider = serviceScope.ServiceProvider;

            var applicationDbContext = scopeServiceProvider.GetService<ApplicationDbContext>();
            await applicationDbContext.Database.MigrateAsync();

            var configurationDbContext = scopeServiceProvider.GetService<ConfigurationDbContext>();
            await configurationDbContext.Database.MigrateAsync();

            var persistedGrantDbContext = scopeServiceProvider.GetService<PersistedGrantDbContext>();
            await persistedGrantDbContext.Database.MigrateAsync();
        }
    }
}