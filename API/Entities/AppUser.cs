using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string KnownAs { get; set; }
		public string UserAvatarUrl { get; set; }
		
		public ICollection<UserBook> BooksRead { get; set; }
		public ICollection<UserFavoriteAuthor> UserFavoriteAuthors { get; set; }
		public ICollection<BookGrade> BookGrades { get; set; }
		public List<Message> MessagesSent { get; set; } = [];
		public List<Message> MessagesReceived { get; set; } = []; 
		
		public ICollection<FriendRequest> SentFriendRequest { get; set; }
		public ICollection<FriendRequest> ReceivedFriendRequest { get; set; }
		public ICollection<Friend> Friends { get; set; }
		
		
		
		
	}
}