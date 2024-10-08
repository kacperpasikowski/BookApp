using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly DataContext _context;
		public BookRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<PagedList<Book>> GetAllBooksAsync(UserParams userParams )
		{
			
			var totalBooks = await _context.Books.CountAsync();
			
			var query = _context.Books
				.Include(b => b.Publisher)
				.Include(b=> b.BookAuthors)
					.ThenInclude(ba => ba.Author)
				.Include(b => b.BookCategories)
					.ThenInclude(bc => bc.Category)
				.AsSplitQuery();

				
			return await PagedList<Book>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<Book> GetBookByIdAsync(Guid id)
		{
			return await _context.Books
				.Include(b => b.Publisher)
				.Include(b=> b.BookAuthors)
					.ThenInclude(ba => ba.Author)
				.Include(b => b.BookCategories)
					.ThenInclude(bc => bc.Category)
				.AsSplitQuery()
				.FirstOrDefaultAsync(b => b.Id == id);
		}


		public async Task<Book> AddBookAsync(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public async Task<Book> UpdateBookAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public async Task<Book> DeleteBookAsync(Guid id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				throw new KeyNotFoundException("Book was not found");
			}
			
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			return book;
		}

        public async Task<IEnumerable<BookGrade>> GetBookGradesAsync(Guid bookId)
        {
            return await _context.BookGrades.Where(bg => bg.BookId == bookId).ToListAsync();
        }
    }
}