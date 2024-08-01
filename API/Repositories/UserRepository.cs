using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class UserRepository : IUserRepository
	{
		
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly DataContext _context;
		
		
		public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}
		
		
		 public async Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams)
		{
			var query =  _context.Users.AsQueryable();
			
			return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<IdentityResult> DeleteUserAsync(AppUser user)
		{
			return await _userManager.DeleteAsync(user);
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