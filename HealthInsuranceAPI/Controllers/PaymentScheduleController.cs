using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentScheduleController : ControllerBase
    {
        PaymentScheduleDB paymentScheduleDB { get; set; }

        public PaymentScheduleController(PaymentScheduleDB _paymentScheduleDB)
        {
            paymentScheduleDB = _paymentScheduleDB;
        }

        [HttpGet("GetPaymentScheduleForUser/{UserDetailID}/{AcquirerPlanID}")]
        public IEnumerable<PaymentSchedule> GetAllPlans(int UserDetailID, int AcquirerPlanID)
        {
            return paymentScheduleDB.GetAll().Where(item => item.UserDetailId == UserDetailID).ToList();
        }

        [HttpPost("GeneratePaymentSchedule")]
        public PageData<PaymentSchedule> GeneratePaymentSchedule(PaymentSchedule paymentSchedule)
        {
            return paymentScheduleDB.GeneratePaymentSchedule(paymentSchedule);
        }
    }
}
