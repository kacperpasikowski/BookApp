using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

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
			var userId = User.GetUserId();

			try
			{
				await _userAuthorService.AddUserFavoriteAuthorAsync(userId, addFavoriteAuthorDto.AuthorId);
				return Ok();
			}
			catch(InvalidOperationException ex)
			{
				return BadRequest(new {message = ex.Message});
			}
			catch (Exception ex)
			{
				throw new ApplicationException( "",ex);
			}


		}
	}
}