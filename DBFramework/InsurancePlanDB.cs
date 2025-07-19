using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
{
    public class InsurancePlanDB : DatabaseLayer<InsurancePlan>
    {
        IDatabaseLayer<InsurancePlan> databaseLayer;
        HealthInsuranceContext context;
        public InsurancePlanDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
            databaseLayer = new DatabaseLayer<InsurancePlan>(context);
        }

        public PageData<InsurancePlan> GetInsurancePlanData(int insurancePlanID)
        {
            var insurancePlan = databaseLayer.GetAll().Where(item => item.InsurancePlanId == insurancePlanID).FirstOrDefault();

            if (insurancePlan != null)
            {
                return new PageData<InsurancePlan>()
                {
                    Data = insurancePlan
                };
            }

            return new PageData<InsurancePlan>
            {
                Message = "No data found"
            };
        }
    }
}
