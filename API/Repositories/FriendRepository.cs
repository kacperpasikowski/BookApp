using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class FriendRepository(DataContext context) : IFriendsRepository
	{
		
		public async Task<FriendRequest> GetFriendRequestAsync(Guid requestId)
		{
			return await context.FriendRequests
				.Include(fr => fr.Requester)
				.Include(fr => fr.Receiver)
				.FirstOrDefaultAsync(fr => fr.Id == requestId);
		}

		public async Task<IEnumerable<FriendRequest>> GetFriendRequestsForUserAsync(Guid userId)
		{
			return await context.FriendRequests
				.Include(fr => fr.Requester)
				.Include(fr=> fr.Receiver)
				.Where(fr => fr.ToUserId == userId && fr.Status == FriendshipStatus.Pending)
				.ToListAsync();
		}

		public async Task<IEnumerable<Friend>> GetFriendsForUserAsync(Guid userId)
		{
			return await context.Friends
				.Where(f => f.UserId1 == userId || f.UserId2 == userId)
				.Include(f => f.User1)
				.Include(f => f.User2)
				.ToListAsync();
		}
		public async Task AddFriendAsync(Friend friend)
		{
			await context.Friends.AddAsync(friend);
		}

		public async Task AddFriendRequestAsync(FriendRequest friendRequest)
		{
			await context.FriendRequests.AddAsync(friendRequest);
		}
		public async Task UpdateFriendRequestAsync(FriendRequest friendRequest)
		{
			context.FriendRequests.Update(friendRequest);
			await context.SaveChangesAsync();
		}

		
		public async Task RemoveFriendRequestAsync(FriendRequest friendRequest)
		{
			context.FriendRequests.Remove(friendRequest);
			await context.SaveChangesAsync();
		}

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}

		public async Task<bool> AreUsersFriendsAsync(Guid userId1, Guid userId2)
		{
			return await context.Friends.AnyAsync(f=>
				(f.UserId1 == userId1 && f.UserId2 == userId2) ||
				(f.UserId1 == userId2 && f.UserId2 == userId1)
			);
		}

		public async Task<bool> IsFriendRequestAlreadySentAsync(Guid fromUserId, Guid toUserId)
		{
			return await context.FriendRequests.AnyAsync(fr=>
			fr.FromUserId == fromUserId &&
			fr.ToUserId == toUserId &&
			fr.Status == FriendshipStatus.Pending
			);
		}
	}
}