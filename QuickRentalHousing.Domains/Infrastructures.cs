using ITHenry.Commons.EntityFrameworkCore.BaseInfrastructures;
using QuickRentalHousing.Domains.Entities.Base;

namespace QuickRentalHousing.Domains.Infrastructures
{
    public class Repository<T> : BaseRepository<QuickRentalHousingDbContext, T>,
        IRepository<T>
        where T : BaseEntity
    {
        public Repository(QuickRentalHousingDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public class UnitOfWork : BaseUnitOfWork<QuickRentalHousingDbContext>,
        IUnitOfWork
    {
        public UnitOfWork(QuickRentalHousingDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public interface IRepository<T> : IBaseRepository<QuickRentalHousingDbContext, T>
        where T : BaseEntity
    {
    }

    public interface IUnitOfWork : IBaseUnitOfWork<QuickRentalHousingDbContext>
    {
    }
}
