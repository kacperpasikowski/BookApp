using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class Friend
	{
		public Guid UserId1 { get; set; }
		public Guid UserId2 { get; set; }
		public DateTime FriendshipDate { get; set; }
		
		
		public AppUser User1 { get; set; }
		public AppUser User2 { get; set; }
	}
}