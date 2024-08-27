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
	public class UserBookRepository : IUserBookRepository
	{
		private readonly DataContext _context;

		public UserBookRepository(DataContext context)
		{
			_context = context;
		}
		
		public async Task<UserBook> GetUserBookAsync(Guid userId, Guid bookId)
        {
            return await _context.UserBooks.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
        }

		public Task<PagedList<UserBook>> GetUserBooksAsync(Guid userId, UserParams userParams)
		{
			var query = _context.UserBooks
				.Where(ub => ub.UserId == userId)
				.Include(ub => ub.Book)
					.ThenInclude(b => b.BookAuthors) 
						.ThenInclude(ba => ba.Author) 
				.Include(ub => ub.Book)
					.ThenInclude(b => b.BookCategories) 
						.ThenInclude(bc => bc.Category) 
				.Include(ub => ub.AppUser) 
				.AsQueryable();
			return PagedList<UserBook>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}
		public async Task AddUserBookAsync(UserBook userBook)
		{
			_context.UserBooks.Add(userBook);
			await _context.SaveChangesAsync();
		}


		public async Task AddOrUpdateBookGradeAsync(BookGrade bookGrade)
		{
			var existingGrade = await _context.BookGrades
				.FirstOrDefaultAsync(bg => bg.UserId == bookGrade.UserId && bg.BookId == bookGrade.BookId);

			if (existingGrade == null)
			{
				_context.BookGrades.Add(bookGrade);
			}
			else
			{
				existingGrade.Grade = bookGrade.Grade;
				_context.BookGrades.Update(existingGrade);
			}

			await _context.SaveChangesAsync();

		}

        
    }
}