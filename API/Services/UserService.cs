using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
	public class UserService : IUserService
	{
		
		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;

		public UserService(IUserRepository userRepository, ITokenService tokenService)
		{
			_userRepository = userRepository;
			_tokenService = tokenService;
		}


		public async Task<IdentityResult> RegisterUserAsync(AppUser user, string password)
		{
			return await _userRepository.RegisterUserAsync(user, password);
		}

		public async Task<string> LoginUserAsync(string userName, string password)
		{
			var result = await _userRepository.PasswordSignInAsync(userName, password);
			if(!result.Succeeded)
			{
				return null;
			}
			
			var user = await _userRepository.FindUserByNameAsync(userName);
			return _tokenService.CreateToken(user);
		}

		
	}
}