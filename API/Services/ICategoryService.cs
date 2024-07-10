using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
	public interface ICategoryService
	{
		Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync();
		Task<GetCategoryDto> GetCategoryByIdAsync(Guid id);
		Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto);
		Task<bool> UpdateCategoryAsync(Guid id, Category category );
		Task<bool> DeleteCategoryAsync(Guid id);
	}
}