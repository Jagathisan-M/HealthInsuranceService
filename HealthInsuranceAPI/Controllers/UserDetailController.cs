using HealthInsuranceAPI.AuthendicationService;
using HealthInsuranceAPI.CoreFrameworkModel;
using HealthInsuranceAPI.DBFramework;
using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace HealthInsuranceAPI.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [ApiController]
    [Produces("application/xml", "application/json")]
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

        //[ApiVersion("1.0")]
        //[ApiVersion("2.0")]
        [HttpGet("ValidateUserWithToken/{UserName}/{Password}")]
        public string ValidateUserWithToken(string UserName, String Password)
        {
            PageData<UserDetail> data = userDetailDB.ValidateUser(UserName, Password);

            if (string.IsNullOrEmpty(data.Message))
            {
                var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, UserName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        [HttpGet("ValidateUser/{UserName}/{Password}")]
        public IActionResult ValidateUser(string UserName, String Password)
        {
            var pageData = userDetailDB.ValidateUser(UserName, Password);
            if (pageData != null && pageData.Data != null)
            {
                var token = tokenService.GenerateToken(pageData.Data.UserDetailId.ToString());
                memoryCacheService.StoreToken(pageData.Data.UserDetailId.ToString(), token);
                pageData.Token = token;
                return Ok(pageData);
            }

            return Unauthorized(new { Message = "UnAuthorized User" });
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

        [HttpGet("GetAllAcquirer/{PageNumber}/{PageSize}"), FormatFilter]
        public PaginationData<UserDetail> GetAllAcquirer(int PageNumber, int PageSize)
        {   
            return userDetailDB.GetAllAcquirer(PageNumber, PageSize);
        }

        [HttpGet("GetUserData/{UserID}")]
        public IActionResult GetUserData(int UserID)
        {
            var data = userDetailDB.Get(UserID);

            if (data.Data != null)
            {
                return Ok(data);
            }

            return NotFound("Data Not Found");
        }

        [HttpPost("FileUpload")]
        public void FileUpload(IFormFile[] files)
        {
            foreach (var item in files)
            {
                string filePath = "C://Files/"+item.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    item.CopyTo(stream);
                }

                using (var stream = new MemoryStream())
                {
                    item.CopyTo(stream);
                    var fileContent = stream.ToArray(); // to store byte[] into sql DB

                    File(fileContent, item.ContentType, item.FileName); // convert byte[] to file to download
                }                
            }
        }

        [HttpGet("~/Test/TestMethod")]
        public string[] TestMethod()
        {
            return ["A", "B", "C"];
        }
    }
}
