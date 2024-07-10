using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class GetAuthorDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string AuthorAvatarUrl { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public List<AuthorsBookDto> Books { get; set; }
	}
}