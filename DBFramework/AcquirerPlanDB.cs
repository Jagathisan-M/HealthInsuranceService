using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
{
    public class AcquirerPlanDB : Repository<AcquirerPlan>
    {
        HealthInsuranceContext context;
        public AcquirerPlanDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
        }

        public PageData<AcquirerPlan> GetUserPlans(int UserDetailID)
        {
            var acquirerPlan = GetAll().Where(item => item.UserDetailId == UserDetailID).FirstOrDefault();

            if (acquirerPlan != null)
            {
                return new PageData<AcquirerPlan>()
                {
                    Data = acquirerPlan
                };
            }

            return new PageData<AcquirerPlan>
            {
                Message = "No data found"
            };
        }
    }
}
