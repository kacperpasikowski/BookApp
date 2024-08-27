using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FriendsController(IFriendService friendService) : ControllerBase
	{
		[HttpPost("send")]
		public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
		{
			if (request == null || request.ToUserId == Guid.Empty)
			{
				return BadRequest("Invalid user ID.");
			}

			var userId = User.GetUserId();
			await friendService.SendFriendRequestAsync(userId, request.ToUserId);
			return Ok();
		}

		[HttpPost("accept/{requestId}")]
		public async Task<IActionResult> AcceptFriendRequest(Guid requestId)
		{
			await friendService.AcceptFriendRequestAsync(requestId);
			return Ok();
		}

		[HttpPost("reject/{requestId}")]
		public async Task<IActionResult> RejectFriendRequest(Guid requestId)
		{
			await friendService.RejectFriendRequestAsync(requestId);
			return Ok();
		}

		[HttpGet("pending")]
		public async Task<IActionResult> GetPendingFriendRequests()
		{
			var userId = User.GetUserId();
			var requests = await friendService.GetPendingFriendRequestsAsync(userId);
			return Ok(requests);
		}

		[HttpGet]
		public async Task<IActionResult> GetFriends()
		{
			var userId = User.GetUserId();
			var friends = await friendService.GetFriendsAsync(userId);
			return Ok(friends);
		}

	}
}