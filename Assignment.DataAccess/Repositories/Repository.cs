using Assignment.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assignment.DataAccess.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TModel> _dbSet;

        public Repository(DbContext dbContext, DbSet<TModel> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }

        public async Task<IList<TModel>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IList<TModel>> All(Expression<Func<TModel, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TModel> Single(Expression<Func<TModel, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TModel> Single(Expression<Func<TModel, bool>> predicate, Func<IQueryable<TModel>, IQueryable<TModel>> includes)
        {
            return await includes(_dbSet).SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> Any()
        {
            return await _dbSet.AnyAsync();
        }

        public async Task<bool> Any(Expression<Func<TModel, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<TModel> Create(TModel toCreate)
        {
            await _dbSet.AddAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return toCreate;
        }

        public async Task<IList<TModel>> Create(IList<TModel> toCreate)
        {
            await _dbSet.AddRangeAsync(toCreate);
            await _dbContext.SaveChangesAsync();
            return toCreate;
        }

        public async Task<TModel> Update(TModel toUpdate)
        {
            _dbSet.Update(toUpdate);
            await _dbContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task<IList<TModel>> Update(IList<TModel> toUpdate)
        {
            _dbSet.UpdateRange(toUpdate);
            await _dbContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task Delete(TModel toDelete)
        {
            _dbSet.Remove(toDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(IList<TModel> toDelete)
        {
            _dbSet.RemoveRange(toDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
