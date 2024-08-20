using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class SearchResultDto
	{
		public Guid Id { get; set; }
		public string TitleOrAuthorName { get; set; }
		public string Type { get; set; }
		public List<CategoryDto> Categories { get; set; }
		public List<BookAuthorsDto> Authors { get; set; }
		public  string MainCategory { get; set; }
		public string AvatarUrl { get; set; }
	}
}