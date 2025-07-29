using HealthInsuranceAPI.AuthendicationService;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthInsuranceAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserDetailController : ControllerBase
    {
        UserDetailDB userDetailDB { get; set; }
        IConfiguration _configuration;
        TokenService tokenService;
        MemoryCacheService memoryCacheService;

        public UserDetailController(UserDetailDB _userDetailDB, IConfiguration configuration, TokenService _tokenService, MemoryCacheService _memoryCacheService)
        {
            _configuration = configuration;
            userDetailDB = _userDetailDB;
            tokenService = _tokenService;
            memoryCacheService = _memoryCacheService;
        }

        //[HttpGet("ValidateUser/{UserName}/{Password}")]
        //public string ValidateUser(string UserName, String Password)
        //{
        //    PageData<UserDetail> data = userDetailDB.ValidateUser(UserName, Password);

        //    if (string.IsNullOrEmpty(data.Message))
        //    {
        //        var claims = new[] {
        //                        new Claim(JwtRegisteredClaimNames.Sub, UserName),
        //                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //                    };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));

        //        var token = new JwtSecurityToken(
        //            _configuration["Jwt:Issuer"],
        //            _configuration["Jwt:Audience"],
        //            claims,
        //            expires: expires,
        //            signingCredentials: creds
        //        );

        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }
        //    return null;
        //}

        [HttpGet("ValidateUser/{UserName}/{Password}")]
        public PageData<UserDetail> ValidateUser(string UserName, String Password)
        {
            var pageData = userDetailDB.ValidateUser(UserName, Password);
            if (pageData != null && pageData.Data != null)
            {
                var token = tokenService.GenerateToken(pageData.Data.UserDetailId.ToString());
                memoryCacheService.StoreToken(pageData.Data.UserDetailId.ToString(), token);
                pageData.Token = token;
                return pageData;
            }

            throw new UnauthorizedAccessException("UnAuthorized User");
        }

        [HttpPost("Add")]
        public UserDetail Add([FromBody] UserDetail userDetail)
        {
            return userDetailDB.Add(userDetail);
        }

        [HttpPut("Update")]
        public UserDetail Update([FromBody] UserDetail userDetail)
        {
            return userDetailDB.Update(userDetail);
        }

        [HttpDelete("Delete")]
        public UserDetail Delete([FromBody] UserDetail userDetail)
        {
            return userDetailDB.Delete(userDetail);
        }

        [HttpGet("GetAllAcquirer/{PageNumber}/{PageSize}")]
        public PaginationData<UserDetail> GetAllAcquirer(int PageNumber, int PageSize)
        {
            return userDetailDB.GetAllAcquirer(PageNumber, PageSize);
        }

        [HttpGet("GetUserData/{UserID}")]
        public PageData<UserDetail> GetUserData(int UserID)
        {
            return userDetailDB.Get(UserID);
        }
    }
}
