using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class GetUserBooksDto
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public Guid BookId { get; set; }
		public string BookTitle { get; set; }
		
		public DateOnly DateRead { get; set; }
	}
}