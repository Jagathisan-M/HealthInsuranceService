using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
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
