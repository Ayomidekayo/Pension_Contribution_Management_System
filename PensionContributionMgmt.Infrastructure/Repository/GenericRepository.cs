using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Infrastructure.Data;

namespace PensionContributionMgmt.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> AddAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
            else
                return await _dbSet.Where(filter).ToListAsync();

        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();

        }
        //public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbSet.Where(filter).FirstOrDefaultAsync();
        //}

        public async Task<T> UpdateAsync(T dbRecord)
        {

            _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }
    }
}



