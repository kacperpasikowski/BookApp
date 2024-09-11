using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
	public class UserService : IUserService
	{

		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserBookRepository _userBookRepository;
		private readonly IFriendService _friendService;
		private readonly IUserAuthorRepository _userAuthorRepository;

		public UserService(IUserRepository userRepository, ITokenService tokenService, UserManager<AppUser> userManager,
		IUserBookRepository userBookRepository, IUserAuthorRepository userAuthorRepository, IFriendService friendService)
		{
			_userRepository = userRepository;
			_tokenService = tokenService;
			_userManager = userManager;
			_userBookRepository = userBookRepository;
			_userAuthorRepository = userAuthorRepository;
			_friendService = friendService;
		}

		public async Task<PagedList<GetAllUsersDto>> GetAllUsersAsync(UserParams userParams)
		{
			var users = await _userRepository.GetAllUsersAsync(userParams);

			var userDtos = new List<GetAllUsersDto>();
			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				var readBooks = await GetReadBooksAsync(user.Id);
				var favoriteAuthors = await GetFavoriteAuthorsAsync(user.Id);
				var friends = await _friendService.GetFriendsAsync(user.Id);



				var userDto = new GetAllUsersDto
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					UserAvatarUrl = user.UserAvatarUrl,
					Roles = roles.ToList(),
					ReadBooks = readBooks,
					FavoriteAuthors = favoriteAuthors,
					Friends = friends.ToList()

				};
				userDtos.Add(userDto);
			}

			return new PagedList<GetAllUsersDto>(userDtos, users.TotalCount, users.CurrentPage, users.PageSize);
		}

		public async Task<GetAllUsersDto> GetUserByUsername(string userName)
		{
			var user = await _userRepository.FindUserByNameAsync(userName);

			if (user == null)
			{
				return null;
			}

			var roles = await _userManager.GetRolesAsync(user);
			var readBooks = await GetReadBooksAsync(user.Id);
			var favoriteAuthors = await GetFavoriteAuthorsAsync(user.Id);
			var friends = await _friendService.GetFriendsAsync(user.Id);




			return new GetAllUsersDto
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				UserAvatarUrl = user.UserAvatarUrl,
				Roles = roles.ToList(),
				ReadBooks = readBooks,
				FavoriteAuthors = favoriteAuthors,
				Friends = friends.ToList()
			};

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

			if (!result.Succeeded)
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
			if (!result.Succeeded)
			{
				return null;
			}

			var user = await _userRepository.FindUserByNameAsync(loginModel.UserName);
			var roles = await _userManager.GetRolesAsync(user);
			var token = await _tokenService.CreateToken(user);

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
			if (user == null)
			{
				return ServiceResult.Failure($"User with name {userName} doesn't exist");
			}

			var result = await _userRepository.DeleteUserAsync(user);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				return ServiceResult.Failure(errors);
			}
			return ServiceResult.Success();
		}



		//helpers
		public async Task<List<ReadBooksDto>> GetReadBooksAsync(Guid userId)
		{
			var readBooks = await _userBookRepository.GetUserBooksAsync(userId, new UserParams());
			return readBooks
				.Where(rb => rb.Book != null)
				.Select(rb => new ReadBooksDto
				{
					Id = rb.BookId,
					Title = rb.Book.Title,
					AuthorName = rb.Book.BookAuthors.FirstOrDefault()?.Author?.Name ?? "Unknown Author",
					CategoryName = rb.Book.BookCategories.FirstOrDefault()?.Category?.Name ?? "No Category",
					BookAvatarUrl = rb.Book.BookAvatarUrl
				})
				.ToList();
		}

		public async Task<List<FavoriteAuthorsDto>> GetFavoriteAuthorsAsync(Guid userId)
		{
			var favoriteAuthors = await _userAuthorRepository.GetUserFavoriteAuthorsAsync(userId, new UserParams());
			return favoriteAuthors
				.Where(fa => fa.Author != null)
				.Select(fa => new FavoriteAuthorsDto
				{
					Id = fa.AuthorId,
					Name = fa.Author.Name,
					AuthorAvatarUrl = fa.Author.AuthorAvatarUrl
				})
				.ToList();
		}

		public async Task<List<string>> GetFavoriteCategoriesAsync(Guid userId)
		{
			var readBooks = await GetReadBooksAsync(userId);
			var categories = readBooks
				.GroupBy(b => b.CategoryName)
				.OrderByDescending(g => g.Count())
				.Select(g => g.Key)
				.Take(3)
				.ToList();
			
			return categories;
		}
	}
}