using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.helpers;

namespace API.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task<PagedList<Book>> GetAllBooksAsync(UserParams userParams);
		Task<Book> GetBookByIdAsync(Guid id);
		Task<IEnumerable<BookGrade>> GetBookGradesAsync(Guid bookId);
		Task<Book> AddBookAsync(Book book);
		Task<Book> DeleteBookAsync(Guid id);
		Task<Book> UpdateBookAsync(Book book);
		
	}
}