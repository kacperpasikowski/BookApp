using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class AddBookGradeDto
	{
		public Guid BookId { get; set; }
		public int Grade { get; set; }
	}
}