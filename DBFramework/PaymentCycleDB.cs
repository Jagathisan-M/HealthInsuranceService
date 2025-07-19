using HealthInsuranceService.CoreFramework;
using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.HealthInsuranceDBContext;

namespace HealthInsuranceService.DBFramework
{
    public class PaymentCycleDB : DatabaseLayer<PaymentCycle>
    {
        IDatabaseLayer<PaymentCycle> databaseLayer;
        HealthInsuranceContext context;
        public PaymentCycleDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
            databaseLayer = new DatabaseLayer<PaymentCycle>(context);
        }
    }
}
