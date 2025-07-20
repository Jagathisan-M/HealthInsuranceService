using HealthInsuranceAPI.CoreFramework;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.HealthInsuranceDBContext;

namespace HealthInsuranceAPI.DBFramework
{
    public class PaymentCycleDB : Repository<PaymentCycle>
    {
        HealthInsuranceContext context;
        public PaymentCycleDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
