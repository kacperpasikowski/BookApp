using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserAuthorController : ControllerBase
	{
		private readonly IUserAuthorService _userAuthorService;

		public UserAuthorController(IUserAuthorService userAuthorService)
		{
			_userAuthorService = userAuthorService;
		}
		
		[HttpPost]
		public async Task<IActionResult> AddFavoriteAuthor(
			[FromBody] AddFavoriteAuthorDto addFavoriteAuthorDto)
		{
			await _userAuthorService.AddUserFavoriteAuthorAsync(addFavoriteAuthorDto.UserId, addFavoriteAuthorDto.AuthorId);
			return Ok(); 
		}
	}
}