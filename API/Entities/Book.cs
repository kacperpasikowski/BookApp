using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class Book
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateOnly DateOfPublish { get; set; }
		public Guid PublisherId { get; set; }
		public Publisher Publisher { get; set; }
		public string BookAvatarUrl { get; set; }
		public ICollection<BookAuthor> BookAuthors { get; set; }
		public ICollection<BookCategory> BookCategories { get; set; }
		public ICollection<BookGrade> BookGrades { get; set; }
		public ICollection<UserBook> UserBooks { get; set; }
		
		
		
	}
}