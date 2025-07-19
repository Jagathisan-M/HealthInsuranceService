using HealthInsuranceService.CoreFrameworkModel;

namespace HealthInsuranceService.CoreFramework
{
    interface IDatabaseLayer<T> where T : class
    {
        IEnumerable<T> GetAll();
        PaginationData<T> GetAllWithPagination(int PageNumber, int PageSize);
        T Add(T entity);
        T Update(T entty);
        T Delete(T entity);
    }
}
