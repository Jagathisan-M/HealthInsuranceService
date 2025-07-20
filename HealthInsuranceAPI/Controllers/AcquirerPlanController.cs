using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcquirerPlanController : ControllerBase
    {
        AcquirerPlanDB acquirerPlanDB { get; set; }

        public AcquirerPlanController(AcquirerPlanDB _acquirerPlanDB)
        {
            acquirerPlanDB = _acquirerPlanDB;
        }

        [HttpGet("GetAllPlans/{PageNumber}/{PageSize}")]
        public PaginationData<AcquirerPlan> GetAllPlans(int PageNumber, int PageSize)
        {
            return acquirerPlanDB.GetAllWithPagination(PageNumber, PageSize);
        }

        [HttpGet("Get/{acquirerPlanID}")]
        public PageData<AcquirerPlan> Get(int acquirerPlanID)
        {
            return acquirerPlanDB.Get(acquirerPlanID);
        }

        [HttpGet("GetUserPlans/{UserDetailID}")]
        public PageData<AcquirerPlan> GetUserPlans(int UserDetailID)
        {
            return acquirerPlanDB.GetUserPlans(UserDetailID);
        }

        [HttpPost("Add")]
        public AcquirerPlan Add([FromBody] AcquirerPlan acquirerPlan)
        {
            return acquirerPlanDB.Add(acquirerPlan);
        }

        [HttpPut("Update")]
        public AcquirerPlan Update([FromBody] AcquirerPlan acquirerPlan)
        {
            return acquirerPlanDB.Update(acquirerPlan);
        }
    }
}
