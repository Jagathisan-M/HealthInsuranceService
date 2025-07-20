using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceAPI.Controllers
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
