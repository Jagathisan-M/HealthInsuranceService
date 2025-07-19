using HealthInsuranceService.CoreFrameworkModel;
using HealthInsuranceService.DBFramework;
using HealthInsuranceService.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserDetailController : ControllerBase
    {
        UserDetailDB userDetailDB { get; set; }

        public UserDetailController(UserDetailDB _userDetailDB)
        {
            userDetailDB = _userDetailDB;
        }

        [HttpGet("ValidateUser/{UserName}/{Password}")]
        public PageData<UserDetail> ValidateUser(string UserName, String Password)
        {
            return userDetailDB.ValidateUser(UserName, Password);
        }

        [HttpPost("Add")]
        public UserDetail Add([FromBody] UserDetail userDetail)
        {
            return userDetailDB.Add(userDetail);
        }

        [HttpPost("Update")]
        public UserDetail Update([FromBody] UserDetail userDetail)
        {
            return userDetailDB.Update(userDetail);
        }

        [HttpGet("GetAllAcquirer/{PageNumber}/{PageSize}")]
        public PaginationData<UserDetail> GetAllAcquirer(int PageNumber, int PageSize)
        {
            return userDetailDB.GetAllAcquirer(PageNumber, PageSize);
        }

        [HttpGet("GetUserData/{UserID}")]
        public PageData<UserDetail> GetUserData(int UserID)
        {
            return userDetailDB.GetUserData(UserID);
        }
    }
}
