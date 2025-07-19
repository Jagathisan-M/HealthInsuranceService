using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentCycleController : ControllerBase
    {
        PaymentCycleDB paymentCycleDB { get; set; }

        public PaymentCycleController(PaymentCycleDB _paymentCycleDB)
        {
            paymentCycleDB = _paymentCycleDB;
        }

        [HttpPost("GetAll")]
        public IEnumerable<PaymentCycle> GetAll()
        {
            return paymentCycleDB.GetAll();
        }
    }
}
