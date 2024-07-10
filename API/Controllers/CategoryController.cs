using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(Guid id)
		{
			var category = await _categoryService.GetCategoryByIdAsync(id);
			if (category==null)
			{
				return NotFound();
			}
			
			return Ok(category);
		}
		
		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
		{
			if(categoryDto == null)
			{
				return BadRequest();
			}

			
			var category = await _categoryService.AddCategoryAsync(categoryDto);
			return CreatedAtAction(nameof(GetCategoryById), new {id = category.Id}, category);
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] Category category)
		{
			if(category == null)
			{
				return BadRequest();
			}
			
			var success = await _categoryService.UpdateCategoryAsync(id, category);
			if (!success)
			{
				return BadRequest();
			}
			
			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(Guid id)
		{
			var success = await _categoryService.DeleteCategoryAsync(id);
			if(!success)
			{
				return NotFound();
			}

			return NoContent();
		}
		
	}
}