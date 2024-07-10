using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.Interfaces
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAllAuthorsAsync();
		Task<Author> GetAuthorByIdAsync(Guid id);
		Task<Author> AddAuthorAsync(Author author);
		Task<Author> UpdateAuthorAsync(Author author);
		Task<Author> DeleteAuthorAsync(Guid id);
	}
}