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
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;
		
		
		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllAuthors()
		{
			var authors = await _authorService.GetAllAuthorsAsync();
			
			return Ok(authors);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAuthorById(Guid id)
		{
			var author = await _authorService.GetAuthorByIdAsync(id);
			
			if (author == null)
			{
				return NotFound();
			}
			
			return Ok(author);
		}
		
		[HttpPost]
		public async Task<IActionResult> AddAuthor ([FromBody] AddAuthorDto addAuthorDto)
		{
			if (addAuthorDto == null)
			{
				return BadRequest();
			}
			
			var authorDto = await _authorService.AddAuthorAsync(addAuthorDto);
			
			return CreatedAtAction(nameof(GetAuthorById), new {id = authorDto.Id}, authorDto);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(Guid id)
		{
			var success = await _authorService.DeleteAuthorAsync(id);
			if (!success)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}