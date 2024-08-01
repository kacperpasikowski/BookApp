using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
	public class TokenService(IConfiguration config, UserManager<AppUser> userManager) : ITokenService
	{
		
		
		public async Task<string> CreateToken(AppUser user)
		{
			var tokenKey = config["TokenKey"] ?? throw new Exception("cannot access token from appsettings");
			
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
			
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, user.UserName)
			};
			
			var roles = await userManager.GetRolesAsync(user);
			
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			
			
			
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = creds
			};
			
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}