using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using R.Entities.Repositories;

namespace R.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>  where TEntity : class
    {
        public DbSet<TEntity> Entities => DbContext.Set<TEntity>();

        public DbContext DbContext { get; private set; }

        public Repository(DbContext context)
        {
            DbContext = context;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression);
        }

        public void Add(TEntity entity, bool saveChanges = true)
        {
            Entities.Add(entity);

            if (saveChanges)
            {
                DbContext.SaveChanges();
            }
        }

        public void Update(TEntity entity, bool saveChanges = true)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }

            DbContext.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                DbContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity, bool saveChanges = true)
        {
            Entities.Remove(entity);

            if (saveChanges)
            {
                DbContext.SaveChanges();
            }
        }

        public async Task AddAsync(TEntity entity, bool saveChanges = true)
        {
            await Entities.AddAsync(entity);

            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }

            DbContext.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            Entities.Remove(entity);

            if (saveChanges)
            {
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
