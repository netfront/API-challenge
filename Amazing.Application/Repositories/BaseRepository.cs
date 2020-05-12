using Amazing.Persistence;
using Amazing.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazing.Application.Repositories
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> GetAll(string include);

        Task<List<TEntity>> GetAll(IEnumerable<string> includes);

        TEntity Get(TKey id);

        TEntity Get(TKey id, string include);

        Task<TEntity> Get(TKey id, IEnumerable<string> includes);

        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TKey id);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task<bool> SaveChangesAsync();

        Task<TEntity> GetAsync(TKey id);
    }

    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        internal readonly AmazingContext dbContext;

        protected BaseRepository(AmazingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return this.dbContext.Set<TEntity>().ToListAsync();
        }

        public Task<List<TEntity>> GetAll(string include)
        {
            return this.dbContext.Set<TEntity>().Include(include).ToListAsync();
        }

        public Task<List<TEntity>> GetAll(IEnumerable<string> includes)
        {
            var query = this.dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.ToListAsync();
        }

        public virtual TEntity Get(TKey id)
        {
            return this.dbContext.Set<TEntity>().SingleOrDefault(c => c.Id.Equals(id));
        }

        public virtual Task<TEntity> GetAsync(TKey id)
        {
            return this.dbContext.Set<TEntity>().SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public TEntity Get(TKey id, string include)
        {
            return this.dbContext.Set<TEntity>().Include(include).SingleOrDefault(c => c.Id.Equals(id));
        }

        public Task<TEntity> Get(TKey id, IEnumerable<string> includes)
        {
            var query = this.dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Add(entity);
            this.dbContext.SaveChanges();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.dbContext.Set<TEntity>().AddRange(entities);
            this.dbContext.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            var entity = new TEntity { Id = id };
            this.dbContext.Set<TEntity>().Attach(entity);
            this.dbContext.Set<TEntity>().Remove(entity);
            this.dbContext.SaveChanges();
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await this.dbContext.SaveChangesAsync()) > 0;
        }

        public virtual void Update(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            this.dbContext.Set<TEntity>().UpdateRange(entities);
            this.dbContext.SaveChanges();
        }
    }
}