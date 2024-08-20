using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	public class RegisterModel
	{
		[Required]
		public string UserName { get; set; }
		
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string UserAvatarUrl { get; set; }
		
		[Required]
		public string Password { get; set; }
		
	}
}