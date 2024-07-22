using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.helpers;
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
		public async Task<IActionResult> GetAllBooks([FromQuery]UserParams userParams)
		{
			var books = await _bookService.GetAllBooksAsync(userParams);
			
			Response.AddPaginationHeader(books);
			
			return Ok(books);
		}
		[HttpGet("search")]
		public async Task<IActionResult> SearchBook([FromQuery] UserParams userParams, [FromQuery] string query)
		{
			var pagedBooks = await _bookService.SearchBooksAsync(userParams, query);
			return Ok(pagedBooks);
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
		[HttpPut("{id}")]
		public async Task<ActionResult<GetBookDto>> UpdateBook(Guid id, [FromBody] AddBookDto addBookDto)
		{
			
			
			if(addBookDto == null)
			{
				return BadRequest("book was not found");
			}
			
			var updatedBook = await _bookService.UpdateBookAsync(id, addBookDto);
			return Ok(updatedBook);
			
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