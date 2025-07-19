using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurancePlanController : ControllerBase
    {
        InsurancePlanDB insurancePlanDB { get; set; }

        public InsurancePlanController(InsurancePlanDB _insurancePlanDB)
        {
            insurancePlanDB = _insurancePlanDB;
        }

        [HttpPost("Get/{insurancePlanID}")]
        public PageData<InsurancePlan> Get(int insurancePlanID)
        {
            return insurancePlanDB.GetInsurancePlanData(insurancePlanID);
        }

        [HttpPost("GetAll/{PageNumber}/{PageSize}")]
        public PaginationData<InsurancePlan> GetAll(int PageNumber, int PageSize)
        {
            return insurancePlanDB.GetAllWithPagination(PageNumber, PageSize);
        }

        [HttpPost("Add")]
        public InsurancePlan Add([FromBody] InsurancePlan insurancePlan)
        {
            return insurancePlanDB.Add(insurancePlan);
        }

        [HttpPost("Update")]
        public InsurancePlan Update([FromBody] InsurancePlan insurancePlan)
        {
            return insurancePlanDB.Update(insurancePlan);
        }
    }
}
