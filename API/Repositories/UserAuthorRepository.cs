using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class UserAuthorRepository : IUserAuthorRepository
	{
		private readonly DataContext _context;

		public UserAuthorRepository(DataContext context)
		{
			_context = context;
		}


		public Task<PagedList<UserFavoriteAuthor>> GetUserFavoriteAuthorsAsync(Guid userId, UserParams userParams)
		{
			var query = _context.UserFavoriteAuthors
								.Where(ufa => ufa.UserId == userId)
								.Include(ufa => ufa.Author)
								.AsQueryable();

			return PagedList<UserFavoriteAuthor>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<UserFavoriteAuthor> GetUserFavoriteAuthorAsync(Guid userId, Guid authorId)
		{
			return await _context.UserFavoriteAuthors.FirstOrDefaultAsync(fa => fa.UserId == userId && fa.AuthorId == authorId);
		}


		public async Task AddFavoriteAuthorAsync(UserFavoriteAuthor userFavoriteAuthor)
		{
			_context.UserFavoriteAuthors.Add(userFavoriteAuthor);
			await _context.SaveChangesAsync();
		}


	}
}