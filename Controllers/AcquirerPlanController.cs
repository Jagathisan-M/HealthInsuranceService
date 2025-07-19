using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceService.Controllers
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

        [HttpPost("GetAllPlans/{PageNumber}/{PageSize}")]
        public PaginationData<AcquirerPlan> GetAllPlans(int PageNumber, int PageSize)
        {
            return acquirerPlanDB.GetAllWithPagination(PageNumber, PageSize);
        }

        [HttpPost("Get/{acquirerPlanID}")]
        public PageData<AcquirerPlan> Get(int acquirerPlanID)
        {
            return acquirerPlanDB.GetAcquirerPlanData(acquirerPlanID);
        }

        [HttpPost("GetUserPlans/{UserDetailID}")]
        public PageData<AcquirerPlan> GetUserPlans(int UserDetailID)
        {
            return acquirerPlanDB.GetUserPlans(UserDetailID);
        }

        [HttpPost("Add")]
        public AcquirerPlan Add([FromBody] AcquirerPlan acquirerPlan)
        {
            return acquirerPlanDB.Add(acquirerPlan);
        }

        [HttpPost("Update")]
        public AcquirerPlan Update([FromBody] AcquirerPlan acquirerPlan)
        {
            return acquirerPlanDB.Update(acquirerPlan);
        }
    }
}
