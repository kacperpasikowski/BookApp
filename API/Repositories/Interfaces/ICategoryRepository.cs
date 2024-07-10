using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAllCategoriesAsync();
		Task<Category> GetCategoryByIdAsync(Guid id);
		Task<Category> AddCategoryAsync(Category category);
		Task<Category> UpdateCategoryAsync(Category category);
		Task<Category> DeleteCategoryAsync(Guid id);
		
	}
}