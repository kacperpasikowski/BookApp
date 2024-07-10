using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class BookCategory
	{
		public Book Book { get; set; }
		public Guid BookId { get; set; }
		public Category Category { get; set; }
		public Guid CategoryId { get; set; }
	}
}