using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class Publisher
	{
		public Guid Id  { get; set; }
		public string Name { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}