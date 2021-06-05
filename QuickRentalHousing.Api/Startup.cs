using ITHenry.Commons.DependencyInjection.Extensions;
using ITHenry.Commons.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuickRentalHousing.Domains;
using QuickRentalHousing.Domains.Infrastructures;
using QuickRentalHousing.Services.Masters;
using System;

namespace QuickRentalHousing.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });
            services.AddControllers()
                .AddMvcOptions(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(
                        new SlugifyParameterTransformer()));
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuickRentalHousing.Api", Version = "v1" });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var assemblyNameOfDomainProject = typeof(DesignTimeDbContextFactory).Assembly.GetName().Name;
            services.AddDbContext<QuickRentalHousingDbContext>(
                dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("QuickRentalHousing.Api"),
                sqlServerOptions => sqlServerOptions.MigrationsAssembly(assemblyNameOfDomainProject)));

            services.AdddBaseInfrastructuresOfEntityFrameworkCoreAsScoped();
            services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>),
                ServiceLifetime.Scoped));

            services.RegisterNonGenericClassesInAssemblyAsScoped(assemblyNameOfDomainProject);

            var assemblyNameOfServiceProject = typeof(ITenantsService).Assembly.GetName().Name;
            services.RegisterNonGenericClassesInAssemblyAsScoped(assemblyNameOfServiceProject);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickRentalHousing.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var dbInitialization = new DbInitialization(app.ApplicationServices);
            dbInitialization.InitializeAndSeedDataAsync().Wait();
        }
    }
}
