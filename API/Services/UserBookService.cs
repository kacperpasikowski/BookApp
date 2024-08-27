using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class UserBookService : IUserBookService
	{
		private readonly IUserBookRepository _userBookRepository;

		public UserBookService(IUserBookRepository userBookRepository)
		{
			_userBookRepository = userBookRepository;
		}

		public async Task AddOrUpdateGradeAsync(Guid userId, Guid bookId, int grade)
		{
			var bookGrade = new BookGrade
			{
				UserId = userId,
				BookId = bookId,
				Grade = grade
			};
			await _userBookRepository.AddOrUpdateBookGradeAsync(bookGrade);
		}

		public async Task AddUserBookAsync(Guid userId, Guid bookId, DateOnly dateRead)
		{
			var existingUserBook = await _userBookRepository.GetUserBookAsync(userId, bookId );
			
			if(existingUserBook != null)
			{
				throw new InvalidOperationException("this book is already in your collection.");
			}
			
			var userBook = new UserBook
			{
				UserId = userId,
				BookId = bookId,
				DateRead = dateRead
				
			};
			await _userBookRepository.AddUserBookAsync(userBook);
		}

		public async Task<PagedList<GetUserBooksDto>> GetUserBooksAsync(Guid userId, UserParams userParams)
		{
			var userBooks = await _userBookRepository.GetUserBooksAsync(userId, userParams);
			
			
			
			var userBookDtos = userBooks.Select(ub => new GetUserBooksDto
			{
				UserId = ub.UserId,
				UserName = ub.AppUser.UserName,
				BookId = ub.BookId,
				BookTitle = ub.Book.Title,
				DateRead = ub.DateRead
			}).ToList();
			
			return new PagedList<GetUserBooksDto>(userBookDtos, userBooks.TotalCount, userBooks.CurrentPage, userBooks.PageSize);
		}
	}
}