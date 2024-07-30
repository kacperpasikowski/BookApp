using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
	public class UserRepository : IUserRepository
	{
		
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		
		public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		
		public async Task<IdentityResult> RegisterUserAsync(AppUser user, string password)
		{
			return await _userManager.CreateAsync(user, password);
		}

		public async Task<AppUser> FindUserByNameAsync(string userName)
		{
			return await _userManager.FindByNameAsync(userName);
		}

		public async Task<SignInResult> PasswordSignInAsync(string userName, string password)
		{
			return await _signInManager.PasswordSignInAsync(userName, password, false, false);
		}

		
	}
}