using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceAPI.Controllers
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

        [HttpGet("Get/{insurancePlanID}")]
        public PageData<InsurancePlan> Get(int insurancePlanID)
        {
            return insurancePlanDB.Get(insurancePlanID);
        }

        [HttpGet("GetAll/{PageNumber}/{PageSize}")]
        public PaginationData<InsurancePlan> GetAll(int PageNumber, int PageSize)
        {
            return insurancePlanDB.GetAllWithPagination(PageNumber, PageSize);
        }

        [HttpPost("Add")]
        public InsurancePlan Add([FromBody] InsurancePlan insurancePlan)
        {
            return insurancePlanDB.Add(insurancePlan);
        }

        [HttpPut("Update")]
        public InsurancePlan Update([FromBody] InsurancePlan insurancePlan)
        {
            return insurancePlanDB.Update(insurancePlan);
        }
    }
}
