using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class Author
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string AuthorAvatarUrl { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public ICollection<BookAuthor> BookAuthors { get; set; }
		public ICollection<UserFavoriteAuthor> UserFavoriteAuthors { get; set; }
		
	}
}