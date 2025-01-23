using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserPanel.Shared.Models;

namespace UserPanel.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _key = config["JwtSettings:SecretKey"] ?? throw new ArgumentNullException(nameof(config), "JwtSettings:SecretKey is required");
            _issuer = config["JwtSettings:Issuer"] ?? throw new ArgumentNullException(nameof(config), "JwtSettings:Issuer is required");
            _audience = config["JwtSettings:Audience"] ?? throw new ArgumentNullException(nameof(config), "JwtSettings:Audience is required");
            _config = config;
        }

        public string GenerateToken(ApplicationUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentNullException(nameof(user.UserName), "UserName is required to generate a token.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName ?? ""),
                new Claim("LastName", user.LastName ?? ""),
                new Claim("CodeMeli", user.CodeMeli ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:DurationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
