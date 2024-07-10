using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class UserFavoriteAuthor
	{
		public AppUser AppUser { get; set; }
		public Guid UserId { get; set; }
		public Author Author { get; set; }
		public Guid AuthorId { get; set; }
	}
}