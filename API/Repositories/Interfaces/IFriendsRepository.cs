using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.Interfaces
{
	public interface IFriendsRepository
	{
		Task<FriendRequest> GetFriendRequestAsync(Guid requestId);
		Task<IEnumerable<FriendRequest>> GetFriendRequestsForUserAsync(Guid userId);
		Task<IEnumerable<Friend>> GetFriendsForUserAsync(Guid userId);
		Task AddFriendRequestAsync(FriendRequest friendRequest);
		Task UpdateFriendRequestAsync(FriendRequest friendRequest);
		Task RemoveFriendRequestAsync(FriendRequest friendRequest);
		Task AddFriendAsync(Friend friend);
		Task SaveChangesAsync();
	}
}