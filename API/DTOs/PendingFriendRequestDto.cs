using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class PendingFriendRequestDto
	{
		public Guid Id { get; set; }
		public Guid FromUserId { get; set; }
		public Guid ToUserId { get; set; }
	}
}