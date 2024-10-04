using Microsoft.IdentityModel.Tokens;
using Real_Time_Chat_Application.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Real_Time_Chat_Application.Utility
{
    public class Token
    {
        private readonly string _secretKey;

        public Token(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
        }

        public string GenerateJwtToken(User user)
        {
            // Define claims
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
            // Uncomment to use username instead
            // new Claim(ClaimTypes.Name, user.Username)
        };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
