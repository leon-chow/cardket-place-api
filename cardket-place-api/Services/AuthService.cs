using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cardket_place_api.Services
{
    public class AuthService
    {
        private IConfiguration _configuration;
        public AuthService(IConfiguration config) { 
            _configuration = config;
        }
        public JwtSecurityToken GenerateJWTToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_SECRET"]));
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:4200",
                expires: DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.Aes128CbcHmacSha256)
            );
            return token;
        }
    }
}
