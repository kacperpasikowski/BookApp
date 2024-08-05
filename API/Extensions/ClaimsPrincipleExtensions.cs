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
		public static string GetUserId(this ClaimsPrincipal user)
		{
			var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("cannot get Id from token");
			
			return userId;
		}
	}
}