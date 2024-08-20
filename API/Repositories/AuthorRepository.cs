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
	public class AuthorRepository : IAuthorRepository
	{
		private readonly DataContext _context;
		
		public AuthorRepository(DataContext context)
		{
			_context = context;
		}
		
		public async Task<PagedList<Author>> GetAllAuthorsAsync(UserParams userParams)
		{
			var totalAuthors = await _context.Authors.CountAsync();
			
			var query = _context.Authors
				.Include(a => a.BookAuthors)
				.ThenInclude(ba => ba.Book);
				
				
			return await PagedList<Author>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<Author> GetAuthorByIdAsync(Guid id)
		{
			return await _context.Authors
				.Include(a => a.BookAuthors)
					.ThenInclude(ba => ba.Book)
						.ThenInclude(b =>  b.BookCategories)
							.ThenInclude(bc => bc.Category)
				.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<Author> AddAuthorAsync(Author author)
		{
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			return author;
		}


		public async Task<Author> UpdateAuthorAsync(Author author)
		{
			_context.Authors.Update(author);
			await _context.SaveChangesAsync();
			return author;
		}

		public async Task<Author> DeleteAuthorAsync(Guid id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null)
			{
				throw new KeyNotFoundException("Author was not found");
			}
			
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			
			return author;
		}
	}
}