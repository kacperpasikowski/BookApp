using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public interface IUserAuthorService
	{
		Task AddUserFavoriteAuthorAsync(Guid userId, Guid authorId);
		
	}
}