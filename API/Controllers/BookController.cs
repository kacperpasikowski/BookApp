using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;
		
		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}
		
		
		[HttpGet]
		public async Task<IActionResult> GetAllBooks()
		{
			var books = await _bookService.GetAllBooksAsync();
			
			return Ok(books);
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookById(Guid id)
		{
			var book = await _bookService.GetBookByIdAsync(id);
			
			return Ok(book);
		}
		[HttpPost]
		public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
		{
			if(addBookDto == null)
			{
				return BadRequest();
			}
			
			
			var book = await _bookService.AddBookAsync(addBookDto);
			return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(Guid id)
		{
			var success = await _bookService.DeleteBookAsync(id);
			if(!success)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}