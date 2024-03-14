using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MobileStore.Core.Models;
using MobileStore.Presentation.Api.Models;

namespace MobileStore.Presentation.Api.Helpers
{
    public static class JwtHelper
    {
        public static string CreateJwt(UserModel user, IConfiguration configuration)
        {
            
            // Create claims for the token (you can add more as needed)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var jwtConfig = JwtConfiguration.Create(configuration);

            // Create a security key based on the secret key
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.SecretKey));
            // Create signing credentials using the security key and the algorithm
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            // Create a JWT token with the specified claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set the expiration time
                signingCredentials: signingCredentials
            );

            // Serialize the token to a string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
