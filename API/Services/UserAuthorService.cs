using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class UserAuthorService : IUserAuthorService
	{
		private readonly IUserAuthorRepository _userAuthorRepository;

		public UserAuthorService(IUserAuthorRepository userAuthorRepository)
		{
			_userAuthorRepository = userAuthorRepository;
		}

		public async Task AddUserFavoriteAuthorAsync(Guid userId, Guid authorId)
		{
			var existingFavoriteAuthor = await _userAuthorRepository.GetUserFavoriteAuthorAsync(userId, authorId);
			
			if(existingFavoriteAuthor != null)
			{
				throw new InvalidOperationException("this author is already in your collection");
			}
			
			
			var favoriteAuthor = new UserFavoriteAuthor
			{
				UserId = userId,
				AuthorId = authorId
			};
			await _userAuthorRepository.AddFavoriteAuthorAsync(favoriteAuthor);
		}
	}
}