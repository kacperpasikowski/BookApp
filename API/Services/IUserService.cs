using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
	public interface IUserService
	{
		Task<IdentityResult> RegisterUserAsync(AppUser user, string password);
		Task<string> LoginUserAsync(string userName, string password);
	}
}