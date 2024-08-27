using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.helpers;

namespace API.Repositories.Interfaces
{
	public interface IUserBookRepository
	{
		Task AddUserBookAsync(UserBook userBook);
		Task<PagedList<UserBook>> GetUserBooksAsync(Guid userId, UserParams userParams);
		Task AddOrUpdateBookGradeAsync(BookGrade bookGrade);
		Task<UserBook> GetUserBookAsync(Guid userId, Guid bookId);
		
	}
}