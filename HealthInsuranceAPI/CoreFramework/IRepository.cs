using HealthInsuranceAPI.CoreFrameworkModel;
using System.Linq.Expressions;

namespace HealthInsuranceAPI.CoreFramework
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        PageData<T> Get(long ID);
        PaginationData<T> GetAllWithPagination(int PageNumber, int PageSize, Expression<Func<T, object>> orderByDescending,
                Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entty);
        T Delete(T entity);


        Task<IEnumerable<T>> GetAllAsync();
        Task<PageData<T>> GetAsync(long ID);
        Task<PaginationData<T>> GetAllWithPaginationAsync(int PageNumber, int PageSize);
        Task<T> AddAsync(T entity);
        //Task<T> UpdateAsync(T entty);
        //Task<T> DeleteAsync(T entity);
    }
}
