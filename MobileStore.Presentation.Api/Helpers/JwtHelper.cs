using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MobileStore.Core.Models;

namespace MobileStore.Presentation.Api.Helpers
{
    public static class JwtHelper
    {
        // Replace these values with your own secret key and audience
        public static string SecretKey = "OFFxMmszRmV3azhEMTlIUUJuNmRZSGNDNlVadVZ6N1ZyQm9HWjhaRk1XQQ==";
        public static string Audience = "your_audience";

        public static string CreateJwt(UserModel user)
        {
            // Create a security key based on the secret key
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey));

            // Create signing credentials using the security key and the algorithm
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims for the token (you can add more as needed)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // Create a JWT token with the specified claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                audience: Audience,
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
