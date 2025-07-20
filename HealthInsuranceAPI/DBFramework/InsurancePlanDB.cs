using HealthInsuranceAPI.CoreFramework;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.HealthInsuranceDBContext;

namespace HealthInsuranceAPI.DBFramework
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
