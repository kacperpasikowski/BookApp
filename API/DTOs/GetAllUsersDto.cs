using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class GetAllUsersDto
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public List<string> Roles { get; set; }
		
	}
}