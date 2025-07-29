using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthInsuranceAPI.AuthendicationService
{
    public class TokenService
    {
        IConfiguration configuration;
        public TokenService(IConfiguration _configuration) {
            configuration = _configuration;
        }

        public string GenerateToken(string ID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.NameIdentifier, ID),
                            new Claim(ClaimTypes.Role, "admin")
                        }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Issuer = configuration["jwt:Issuer"],
                Audience = configuration["jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
