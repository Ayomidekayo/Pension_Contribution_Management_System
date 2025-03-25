using System.Linq.Expressions;

namespace PensionContributionMgmt.Application.Infrastructure
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        //Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T dbRecord);
        Task<T> UpdateAsync(T dbRecord);
        Task<bool> DeleteAsync(T dbRecord);
    }
}
