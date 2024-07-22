using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;

namespace API.Services
{
	public interface IBookService
	{
		Task<PagedList<GetBookDto>> GetAllBooksAsync(UserParams userParams);
		Task<PagedList<GetBookDto>> SearchBooksAsync(UserParams userParams, string query);
		Task<GetBookDto> GetBookByIdAsync(Guid id);
		Task<GetBookDto> AddBookAsync(AddBookDto addBookDto);
		Task<GetBookDto> UpdateBookAsync(Guid id, AddBookDto addBookDto);
		
		Task<bool> DeleteBookAsync(Guid id);
	}
}