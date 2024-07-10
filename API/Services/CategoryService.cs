using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync()
		{
			var categories = await _categoryRepository.GetAllCategoriesAsync();
			return categories.Select(category => new GetCategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				Books = category.BookCategories.Select(bc => new CategoryBooksDto
				{
					Id = bc.Book.Id,
					Title = bc.Book.Title,
					DateOfPublish = bc.Book.DateOfPublish
				}).ToList()
			}).ToList();
		}

		public async Task<GetCategoryDto> GetCategoryByIdAsync(Guid id)
		{
			var category = await _categoryRepository.GetCategoryByIdAsync(id);
			if(category == null)
			{
				return null;
			}
			
			return new GetCategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				Books = category.BookCategories.Select(bc => new CategoryBooksDto
				{
					Id = bc.Book.Id,
					Title = bc.Book.Title,
					DateOfPublish = bc.Book.DateOfPublish
				}).ToList() 
			};
			
		}
		public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto)
		{
			var category = new Category
			{
				Id = Guid.NewGuid(),
				Name = categoryDto.Name,
				BookCategories = new List<BookCategory>()
			};
			
			await _categoryRepository.AddCategoryAsync(category);
			return new CategoryDto
			{
				Id = category.Id,
				Name = category.Name
			};
		}

		
		public async Task<bool> UpdateCategoryAsync(Guid id, Category category)
		{
			var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
			
			if(existingCategory == null)
			{
				return false;
			}
			
			existingCategory.Name= category.Name;
			await _categoryRepository.UpdateCategoryAsync(existingCategory);
			return true;
		}
		
		public async Task<bool> DeleteCategoryAsync(Guid id)
		{
			var category = await _categoryRepository.GetCategoryByIdAsync(id);
			if(category == null)
			{
				return false;
			}
			
			await _categoryRepository.DeleteCategoryAsync(id);
			return true;
		}
	}
}