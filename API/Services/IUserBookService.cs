using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;

namespace API.Services
{
	public interface IUserBookService
	{
		Task AddUserBookAsync(Guid userId, Guid bookId, DateOnly dateRead);
		Task<PagedList<GetUserBooksDto>> GetUserBooksAsync(Guid userId, UserParams userParams);
		Task AddOrUpdateGradeAsync(Guid userId, Guid bookId, int grade);
	}
}