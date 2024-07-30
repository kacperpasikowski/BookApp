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
		
		
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var user = new AppUser {UserName = model.UserName, Email = model.Email};
			
			var result = await _userService.RegisterUserAsync(user, model.Password);
			
			if(!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}
			
			return Ok(user);
		}
		
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var token = await _userService.LoginUserAsync(model.UserName, model.Password);
			
			if(token == null)
			{
				return Unauthorized();
			}
			
			return Ok(new {Token = token});
		}
		
		
	}
}