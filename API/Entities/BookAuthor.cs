using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class BookAuthor
	{
		public Book Book { get; set; }
		public Guid BookId { get; set; }
		public Author Author { get; set; }
		public Guid AuthorId { get; set; }
	}
}