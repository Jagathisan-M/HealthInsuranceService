using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceService.DBFramework
{
    public class UserDetailDB : DatabaseLayer<UserDetail>
    {
        IDatabaseLayer<UserDetail> databaseLayer;
        HealthInsuranceContext context;
        public UserDetailDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
            databaseLayer = new DatabaseLayer<UserDetail>(context);
        }

        public PageData<UserDetail> ValidateUser(string UserName, string Password)
        {
            PageData<UserDetail> data = new PageData<UserDetail>();

            UserDetail userDetail = databaseLayer.GetAll().Where(item => item.UserName == UserName && item.Password == Password).FirstOrDefault();

            if (userDetail != null)
            {
                data.Data = userDetail ?? new UserDetail();
            }
            else
            {
                data.Message = "Invalid User";
            }

            return data;
        }

        public PaginationData<UserDetail> GetAllAcquirer(int PageNumber, int PageSize)
        {
            var Acquirers = databaseLayer.GetAll().Where(item => item.IsIssuer ?? false).ToList();

            return new PaginationData<UserDetail>()
            {
                TotalCount = Acquirers.Count(),
                Data = Acquirers.Skip(PageNumber - 1 * PageSize).Take(PageSize).ToList(),
                PageNumber = PageNumber,
                PageSize = PageSize
            };
        }

        public PageData<UserDetail> GetUserData(int UserDetailID)
        {
            var Acquirer = databaseLayer.GetAll().Where(item => item.UserDetailId == UserDetailID).FirstOrDefault();

            if (Acquirer != null)
            {
                return new PageData<UserDetail>()
                {
                    Data = Acquirer
                };
            }

            return new PageData<UserDetail>
            {
                Message = "No data found"
            };
        }
    }
}
