using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities;
using QuickRentalHousing.Domains.Entities.Masters;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QuickRentalHousing.Domains
{
    public class QuickRentalHousingDbContext : DbContext
    {
        public DbSet<District> Districts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Homeowner> Homeowners { get; set; }
        public DbSet<HomeownerEmail> HomeownerEmails { get; set; }
        public DbSet<HomeownerPhone> HomeownerPhones { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantEmail> TenantEmails { get; set; }
        public DbSet<TenantPhone> TenantPhones { get; set; }
        public DbSet<LotHomeowner> LotHomeowners { get; set; }
        public DbSet<Rental01Registration> Rental01Registrations { get; set; }
        public DbSet<Rental02Appointment> Rental02Appointments { get; set; }
        public DbSet<Rental03Contract> Rental03Contracts { get; set; }

        public QuickRentalHousingDbContext([NotNull] DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
