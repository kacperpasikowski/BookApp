using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.helpers;

namespace API.Repositories.Interfaces
{
	public interface IAuthorRepository
	{
		Task<PagedList<Author>> GetAllAuthorsAsync(UserParams userParams);
		Task<Author> GetAuthorByIdAsync(Guid id);
		Task<Author> AddAuthorAsync(Author author);
		Task<Author> UpdateAuthorAsync(Author author);
		Task<Author> DeleteAuthorAsync(Guid id);
	}
}