using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
	public interface IFriendService
	{
		public Task SendFriendRequestAsync(Guid fromUserId, Guid toUserId);
		public Task AcceptFriendRequestAsync(Guid requestId);
		public Task RejectFriendRequestAsync(Guid requestId);
		public Task<IEnumerable<PendingFriendRequestDto>> GetPendingFriendRequestsAsync(Guid userId);
		public Task<IEnumerable<FriendDto>> GetFriendsAsync(Guid userId);
	}
}