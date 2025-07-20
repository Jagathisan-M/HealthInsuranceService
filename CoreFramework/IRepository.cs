using HealthInsuranceService.CoreFrameworkModel;

namespace HealthInsuranceService.CoreFramework
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        PageData<T> Get(long ID);
        PaginationData<T> GetAllWithPagination(int PageNumber, int PageSize);
        T Add(T entity);
        T Update(T entty);
        T Delete(T entity);
    }
}
