using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
{
    public class AcquirerPlanDB : DatabaseLayer<AcquirerPlan>
    {
        IDatabaseLayer<AcquirerPlan> databaseLayer;
        HealthInsuranceContext context;
        public AcquirerPlanDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
            databaseLayer = new DatabaseLayer<AcquirerPlan>(context);
        }

        public PageData<AcquirerPlan> GetAcquirerPlanData(int AcquirerPlanID)
        {
            var acquirerPlan = databaseLayer.GetAll().Where(item => item.AcquirerPlanId == AcquirerPlanID).FirstOrDefault();

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

        public PageData<AcquirerPlan> GetUserPlans(int UserDetailID)
        {
            var acquirerPlan = databaseLayer.GetAll().Where(item => item.UserDetailId == UserDetailID).FirstOrDefault();

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
