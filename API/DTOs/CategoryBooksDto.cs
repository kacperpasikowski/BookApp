using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class CategoryBooksDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public DateOnly DateOfPublish { get; set; }
	}
}