using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthInsuranceAPI.CoreFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        HealthInsuranceContext context;
        DbSet<T> dbset;

        public Repository(HealthInsuranceContext _context)
        {
            context = _context;
            dbset = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public PageData<T> Get(long ID)
        {
            var Data = dbset.Find(ID);

            if (Data != null)
            {
                return new PageData<T>()
                {
                    Data = Data
                };
            }

            return new PageData<T>
            {
                Message = "No data found"
            };
        }

        public PaginationData<T> GetAllWithPagination(int PageNumber, int PageSize, Expression<Func<T, object>> orderByDescending,
                Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.OrderByDescending(orderByDescending);

            var dataCount = query.Count();
            var data = query.Skip(PageNumber - 1 * PageSize).Take(PageSize).ToList();
            return new PaginationData<T>()
            {
                TotalCount = dataCount,
                Data = data,
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<PageData<T>> GetAsync(long ID)
        {
            T data = await dbset.FindAsync(ID);

            if (data != null)
            {
                return new PageData<T>()
                {
                    Data = data
                };
            }

            return new PageData<T>
            {
                Message = "No data found"
            };
        }

        public async Task<PaginationData<T>> GetAllWithPaginationAsync(int PageNumber, int PageSize)
        {
            var dataCount = dbset.CountAsync();
            var data = dbset.Skip(PageNumber - 1 * PageSize).Take(PageSize).ToListAsync();
            await Task.WhenAll(dataCount, data);
            return new PaginationData<T>()
            {
                TotalCount = dataCount.Result,
                Data = data.Result,
                PageNumber = PageNumber,
                PageSize = PageSize
            };
        }

        public async Task<T> AddAsync(T entity)
        {
            dbset.Update(entity).State = EntityState.Modified;

            await dbset.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public Task<PaginationData<T>> GetAllWithPaginationAsync(int PageNumber, int PageSize, Expression<Func<T, object>> orderByDescending, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        //public async Task<T> AddRangeAsync(T entity)
        //{
        //    await dbset.AddRangeAsync(entity);
        //    await context.SaveChangesAsync();
        //    return entity;
        //}
    }
}
