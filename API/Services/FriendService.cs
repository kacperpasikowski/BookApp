using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Repositories;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class FriendService(IFriendsRepository friendRepository) : IFriendService
	{

		public async Task SendFriendRequestAsync(Guid fromUserId, Guid toUserId)
		{
			
			if(fromUserId == toUserId)
			{
				throw new InvalidOperationException("you cannot send a friend request to yourself!");
			}
			
			var isAlreadyFriend = await friendRepository.AreUsersFriendsAsync(fromUserId, toUserId);
			if(isAlreadyFriend)
			{
				throw new InvalidOperationException("you are already friends with this user.");
			}
			
			var isRequestAlreadySent = await friendRepository.IsFriendRequestAlreadySentAsync(fromUserId,toUserId);
			if(isRequestAlreadySent)
			{
				throw new InvalidOperationException("You have already sent a friend request to this user!");
			}

			var friendRequest = new FriendRequest
			{
				Id = Guid.NewGuid(),
				FromUserId = fromUserId,
				ToUserId = toUserId,
				Status = FriendshipStatus.Pending,
				RequestDate = DateTime.Now
			};

			await friendRepository.AddFriendRequestAsync(friendRequest);
			await friendRepository.SaveChangesAsync();
		}
		public async Task AcceptFriendRequestAsync(Guid requestId)
		{
			var friendRequest = await friendRepository.GetFriendRequestAsync(requestId);

			if (friendRequest != null && friendRequest.Status == FriendshipStatus.Pending)
			{
				friendRequest.Status = FriendshipStatus.Accepted;
				await friendRepository.UpdateFriendRequestAsync(friendRequest);

				var friend = new Friend
				{
					UserId1 = friendRequest.FromUserId,
					UserId2 = friendRequest.ToUserId,
					FriendshipDate = DateTime.Now
				};

				await friendRepository.AddFriendAsync(friend);
				await friendRepository.SaveChangesAsync();
			}
		}
		public async Task RejectFriendRequestAsync(Guid requestId)
		{
			var friendRequest = await friendRepository.GetFriendRequestAsync(requestId);

			if (friendRequest != null && friendRequest.Status == FriendshipStatus.Pending)
			{
				friendRequest.Status = FriendshipStatus.Rejected;
				await friendRepository.UpdateFriendRequestAsync(friendRequest);
				await friendRepository.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<PendingFriendRequestDto>> GetPendingFriendRequestsAsync(Guid userId)
		{
			var requests = await friendRepository.GetFriendRequestsForUserAsync(userId);
			
			var requestDtos = requests.Select(r => new PendingFriendRequestDto
			{
				Id = r.Id,
				FromUserId = r.FromUserId,
				FromUserName = r.Requester.UserName,
				FromUserAvatarUrl = r.Requester.UserAvatarUrl,
				ToUserId = r.ToUserId
			});
			
			return requestDtos;
		}

		public async Task<IEnumerable<FriendDto>> GetFriendsAsync(Guid userId)
		{
			var friends = await friendRepository.GetFriendsForUserAsync(userId);

			var friendDtos = friends.Select(f => new FriendDto
			{
				UserId = f.UserId1 == userId ? f.User2.Id : f.User1.Id,
				UserName = f.UserId1 == userId ? f.User2.UserName : f.User1.UserName,
				Email = f.UserId1 == userId ? f.User2.Email : f.User1.Email,
				UserAvatarUrl = f.UserId1 == userId ? f.User2.UserAvatarUrl : f.User1.UserAvatarUrl
			});
			
			return friendDtos;
		}






	}
}