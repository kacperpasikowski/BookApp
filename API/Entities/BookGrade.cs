using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class BookGrade
	{
		public Guid UserId { get; set; }
		public AppUser AppUser { get; set; }
		public Guid BookId { get; set; }
		public Book Book { get; set; }
		public int Grade { get; set; }
	}
}