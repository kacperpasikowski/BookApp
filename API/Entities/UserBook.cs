using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class UserBook
	{
		public AppUser AppUser { get; set; }
		public Guid UserId { get; set; }
		public Book Book { get; set; }
		public Guid BookId { get; set; }
	}
}