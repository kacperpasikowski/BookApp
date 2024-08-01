using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.helpers;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		
		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		
		[HttpGet]
		public async Task<IActionResult>GetAllUsers([FromQuery] UserParams userParams)
		{
			var users = await _userService.GetAllUsersAsync(userParams);
			return Ok(users);
		}
		
		[HttpDelete("delete/{userName}")]
		public async Task<IActionResult> DeleteUser(string userName)
		{
			var result = await _userService.DeleteUserByUserNameAsync(userName);
			if(result.Succeeded)
			{
				return Ok($"{userName} has been deleted");
			}
			
			return BadRequest(result.Errors);
		}
	}
}