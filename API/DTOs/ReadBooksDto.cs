using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class ReadBooksDto
	{
		public string Title { get; set; }
		public string AuthorName { get; set; }
		public string CategoryName { get; set; }
		public string BookAvatarUrl { get; set; }
	}
}