using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string UserAvatarUrl { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public List<string> Roles { get; set; }
		
	}
}