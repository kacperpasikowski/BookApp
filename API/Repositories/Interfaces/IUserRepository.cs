using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.helpers;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams);
		Task<IdentityResult> DeleteUserAsync(AppUser user);
		Task<IdentityResult> RegisterUserAsync(AppUser user, string password);
		
		Task<AppUser> FindUserByNameAsync(string userName);
		Task<SignInResult> PasswordSignInAsync(string userName, string password);
	}
}