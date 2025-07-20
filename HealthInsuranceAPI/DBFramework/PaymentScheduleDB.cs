using HealthInsuranceAPI.CoreFramework;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.HealthInsuranceDBContext;

namespace HealthInsuranceAPI.DBFramework
{
    public class PaymentScheduleDB : Repository<PaymentSchedule>
    {
        HealthInsuranceContext context;
        public PaymentScheduleDB(HealthInsuranceContext _context) : base(_context)
        {
            context = _context;
        }

        public PageData<PaymentSchedule> GeneratePaymentSchedule(PaymentSchedule paymentSchedule)
        {
            switch (paymentSchedule.PaymentCycle?.CycleDescription?.ToUpper() ?? string.Empty)
            {
                case "MONTHLY":
                    break;
                case "QUATERLY":
                    break;
                case "HALFLY":
                    break;
                case "YEARLY":
                    break;
                case "2 YEARS":
                    break;
                case "3 YEARS":
                    break;
                case "5 YEARS":
                    break;

                default:
                    break;
            }

            return null;
        }
    }
}
