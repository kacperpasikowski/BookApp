using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.helpers;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserBookController : ControllerBase
	{
		private readonly IUserBookService _userBookService;

		public UserBookController(IUserBookService userBookService)
		{
			_userBookService = userBookService;
		}
		
		
		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUserBooks(Guid userId, [FromQuery] UserParams userParams)
		{
			var userBooks = await _userBookService.GetUserBooksAsync(userId, userParams);
			return Ok(userBooks);
		}
		
		[HttpPost]
		public async Task<IActionResult> AddUserBook([FromBody] AddUserBookDto addUserBookDto)
		{
			await _userBookService.AddUserBookAsync(addUserBookDto.UserId, addUserBookDto.BookId, addUserBookDto.DateRead);
			return Ok();
		}
		[HttpPost("grade")]
		public async Task<IActionResult> AddOrUpdateBookGrade([FromBody] AddBookGradeDto addBookGradeDto)
		{
			var userId = User.GetUserId();
			
			await _userBookService.AddOrUpdateGradeAsync(userId, addBookGradeDto.BookId,addBookGradeDto.Grade );
			return Ok();
		}
		
		
		
		
	}
}