using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
	public interface IBookService
	{
		Task<IEnumerable<GetBookDto>> GetAllBooksAsync();
		Task<GetBookDto> GetBookByIdAsync(Guid id);
		Task<GetBookDto> AddBookAsync(AddBookDto addBookDto);
		Task<bool> DeleteBookAsync(Guid id);
	}
}