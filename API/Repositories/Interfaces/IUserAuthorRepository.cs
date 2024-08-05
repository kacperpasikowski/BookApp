using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.helpers;

namespace API.Repositories.Interfaces
{
	public interface IUserAuthorRepository
	{
		Task AddFavoriteAuthorAsync(UserFavoriteAuthor userFavoriteAuthor);
		Task<PagedList<UserFavoriteAuthor>> GetUserFavoriteAuthorsAsync(Guid userId, UserParams userParams);
	}
}