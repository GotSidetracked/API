using KTU_API.Autherization.Model;
using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KTU_API.Autherization
{
    public interface ITokenService
    {
        Task<string> CreateUserToken(User user);
    }

    public class TokenService : ITokenService
    {
        private SymmetricSecurityKey _authSigningKey;
        private UserManager<User> _userManager;
        private string _issuer;
        private string _audience;


        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _userManager = userManager;
            _authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _issuer = configuration["JWT:ValidIssuer"];
            _audience = configuration["JWT:ValidAudience"];
        }

        public async Task<string> CreateUserToken(User user)
        {
            var UserRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.UserID, user.Id.ToString())
            };
            authClaims.AddRange(UserRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var acessToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(acessToken);

        }

    }
}
