using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
	public interface IUserService
	{
		Task<UserDto> RegisterUserAsync(RegisterModel registerModel);
		Task<UserDto> LoginUserAsync(LoginModel loginModel);
		Task<ServiceResult> DeleteUserByUserNameAsync(string userName);
		Task<PagedList<GetAllUsersDto>> GetAllUsersAsync(UserParams userParams);
		
	}
}