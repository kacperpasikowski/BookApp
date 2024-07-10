using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;
		
		public CategoryRepository(DataContext context)
		{
			_context = context;
		}
		
		public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
		{
			return await _context.Categories
				.Include(c => c.BookCategories)
					.ThenInclude(bc => bc.Book)
				.ToListAsync();
		}

		public async Task<Category> GetCategoryByIdAsync(Guid id)
		{
			return await _context.Categories
				.Include(c => c.BookCategories)
					.ThenInclude(bc => bc.Book)
				.FirstOrDefaultAsync(c => c.Id == id);
		}
		public async Task<Category> AddCategoryAsync(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}
		public async Task<Category> UpdateCategoryAsync(Category category)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> DeleteCategoryAsync(Guid id)
		{
			var category = await _context.Categories.FindAsync(id);
			if(category == null)
			{
				throw new KeyNotFoundException("Category was not found");
			}
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			
			return category;
		}

		

		
	}
}