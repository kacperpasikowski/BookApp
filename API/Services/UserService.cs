using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
	public class UserService : IUserService
	{
		
		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserBookRepository _userBookRepository;
		private readonly IUserAuthorRepository _userAuthorRepository;

		public UserService(IUserRepository userRepository, ITokenService tokenService, UserManager<AppUser> userManager, 
		IUserBookRepository userBookRepository, IUserAuthorRepository userAuthorRepository)
		{
			_userRepository = userRepository;
			_tokenService = tokenService;
			_userManager = userManager;
			_userBookRepository = userBookRepository;
			_userAuthorRepository = userAuthorRepository;
		}

		public async Task<PagedList<GetAllUsersDto>> GetAllUsersAsync(UserParams userParams)
		{
			var users = await _userRepository.GetAllUsersAsync(userParams);
			
			var userDtos = new List<GetAllUsersDto>();
			foreach(var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				
				var readBooks = await _userBookRepository.GetUserBooksAsync(user.Id, userParams);
				var readBooksTitles = readBooks.Select(rb=> rb.Book.Title).ToList();
				
				var favoriteAuthors = await _userAuthorRepository.GetUserFavoriteAuthorsAsync(user.Id, userParams);
				var favoriteAuthorsNames = favoriteAuthors.Select(fa => fa.Author.Name).ToList();
				
				var userDto = new GetAllUsersDto
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					UserAvatarUrl = user.UserAvatarUrl,
					Roles = roles.ToList(),
					ReadBooks = readBooksTitles,
					FavoriteAuthors = favoriteAuthorsNames
					
				};
				userDtos.Add(userDto);
			}
			
			return new PagedList<GetAllUsersDto>(userDtos, users.TotalCount, users.CurrentPage, users.PageSize);
		}
		
		
		public async Task<UserDto> RegisterUserAsync(RegisterModel registerModel)
		{
			var user = new AppUser
			{
				UserName = registerModel.UserName,
				Email = registerModel.Email,
				UserAvatarUrl = registerModel.UserAvatarUrl
			};
			
			var result = await _userRepository.RegisterUserAsync(user, registerModel.Password);
			
			if(!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new Exception(string.Join(", ", errors));
			}
			
			
			
			await _userManager.AddToRoleAsync(user, "User");
			
			var roles = await _userManager.GetRolesAsync(user);
			var token = await _tokenService.CreateToken(user);
			return new UserDto
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Token = token,
				UserAvatarUrl = user.UserAvatarUrl,
				Roles = roles.ToList()
			};
			
			
		}

		public async Task<UserDto> LoginUserAsync(LoginModel loginModel)
		{
			var result = await _userRepository.PasswordSignInAsync(loginModel.UserName, loginModel.Password);
			if(!result.Succeeded)
			{
				return null;
			}
			
			var user = await _userRepository.FindUserByNameAsync(loginModel.UserName);
			var roles = await _userManager.GetRolesAsync(user);
			var token = await  _tokenService.CreateToken(user);
			
			return new UserDto
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				UserAvatarUrl = user.UserAvatarUrl,
				Token = token,
				Roles = roles.ToList()
				
				
			};
		}

		public async Task<ServiceResult> DeleteUserByUserNameAsync(string userName)
		{
			var user = await _userRepository.FindUserByNameAsync(userName);
			if(user == null)
			{
				return ServiceResult.Failure($"User with name {userName} doesn't exist");
			}
			
			var result = await _userRepository.DeleteUserAsync(user);
			
			if(!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				return ServiceResult.Failure(errors);
			}
			return ServiceResult.Success();
		}

		
	}
}