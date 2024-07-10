using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class GetBookDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateOnly DateOfPublish { get; set; }
		public string PublisherName { get; set; }
		public string BookAvatarUrl { get; set; }
		public List<BookAuthorsDto> Authors { get; set; }
		public List<CategoryDto> Categories { get; set; }
	}
}