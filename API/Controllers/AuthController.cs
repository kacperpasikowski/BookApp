using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;
		
		public AuthController(IUserService userService)
		{
			_userService = userService;
		}
		
		//https://localhost:5001/auth/register
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
		{
			try
			{
				var userDto = await _userService.RegisterUserAsync(registerModel);
				return Ok(userDto);
			}
			catch(Exception ex)
			{
				return BadRequest(new {Errors = ex.Message});
			}
			
		}
		
		//https://localhost:5001/auth/login
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			var result = await _userService.LoginUserAsync(loginModel);
			
			if(result == null)
			{
				return Unauthorized(new {Errors = "Invalid login attempt"});
			}
			
			return Ok(result);
		}
		
		
	}
}