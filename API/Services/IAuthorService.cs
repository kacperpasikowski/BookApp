using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.helpers;

namespace API.Services
{
	public interface IAuthorService
	{
		Task<PagedList<GetAuthorDto>> GetAllAuthorsAsync(UserParams userParams);
		Task<GetAuthorDto> GetAuthorByIdAsync(Guid id);
		Task<string> GetAuthorMainCategory (Guid id);
		Task<AddAuthorDto> AddAuthorAsync(AddAuthorDto addAuthorDto);
		Task<bool> DeleteAuthorAsync(Guid id);
	}
}