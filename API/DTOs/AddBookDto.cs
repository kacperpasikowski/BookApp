using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class AddBookDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateOnly DateOfPublish { get; set; }
		public Guid PublisherId { get; set; }
		public string BookAvatarUrl { get; set; }
		public List<Guid> AuthorIds { get; set; }
		public List<Guid> CategoryIds { get; set; }
	}
}