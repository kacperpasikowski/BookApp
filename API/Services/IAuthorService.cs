using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Services
{
	public interface IAuthorService
	{
		Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync();
		Task<GetAuthorDto> GetAuthorByIdAsync(Guid id);
		Task<AddAuthorDto> AddAuthorAsync(AddAuthorDto addAuthorDto);
		Task<bool> DeleteAuthorAsync(Guid id);
	}
}