using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceService.CoreFramework
{
    public class DatabaseLayer<T> : IDatabaseLayer<T> where T : class
    {
        HealthInsuranceContext context;
        DbSet<T> dbset;

        public DatabaseLayer(HealthInsuranceContext _context)
        {
            context = _context;
            dbset = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public PaginationData<T> GetAllWithPagination(int PageNumber, int PageSize)
        {
            var allData = dbset.ToList();
            return new PaginationData<T>()
            {
                TotalCount = allData.Count(),
                Data = allData.Skip(PageNumber - 1 * PageSize).Take(PageSize).ToList(),
                PageNumber = PageNumber,
                PageSize = PageSize
            };
        }

        public T Add(T entity)
        {
            dbset.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            dbset.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            dbset.Remove(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
