using LocalFarmerMarket.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace LocalFarmerMarket.API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {

   

                        var claims = new[]
                                    {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                                new Claim(JwtRegisteredClaimNames.Azp, user.Role)
                            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiresIn = int.Parse(_config["Jwt:ExpiresInHours"]);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expiresIn),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}

