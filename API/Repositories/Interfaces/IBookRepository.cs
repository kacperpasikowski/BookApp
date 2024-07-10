using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<Book> GetBookByIdAsync(Guid id);
		Task<Book> AddBookAsync(Book book);
		Task<Book> DeleteBookAsync(Guid id);
		Task<Book> UpdateBookAsync(Book book);
		
	}
}