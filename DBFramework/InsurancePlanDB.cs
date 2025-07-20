using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
{
    public class InsurancePlanDB : Repository<InsurancePlan>
    {
        HealthInsuranceContext context;
        public InsurancePlanDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
