using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class FriendRequest
	{
		public Guid Id { get; set; }
		public Guid FromUserId { get; set; }
		public Guid ToUserId { get; set; }
		public FriendshipStatus Status { get; set; }
		public DateTime RequestDate { get; set; }
		
		public AppUser Requester { get; set; }
		public AppUser Receiver { get; set; }
	}
	public enum FriendshipStatus
	{
		Pending,
		Accepted,
		Rejected
	}
}