using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using R.Entities.Repositories;

namespace R.Entities.UnitOfWorks
{
    public interface IUnitOfWork
    {
        DbContext DbContext { get; }

        int SaveChanges();
        Task SaveChangesAsync();
        void Dispose();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task BeginTransaction();
        Task Commit();
        Task RollBack();
    }
}
