using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class ClaimsPrincipleExtensions
	{
		public static string GetUsername(this ClaimsPrincipal user)
		{
			var username = user.FindFirstValue(ClaimTypes.Name) ?? throw new Exception("cannot get username from token");
			
			return username;
		}
		public static Guid GetUserId(this ClaimsPrincipal user)
		{
			var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
			
			if(string.IsNullOrEmpty(userIdString)|| !Guid.TryParse(userIdString, out var userId))
			{
				throw new Exception("cannot get Id from Token");
			}
			
			return userId;
			
		}
	}
}