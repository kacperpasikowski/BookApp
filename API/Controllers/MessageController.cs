using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MessageController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IMessageRepository _messageRepository;

		public MessageController(IMapper mapper, IUserRepository userRepository, IMessageRepository messageRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_messageRepository = messageRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto createMessageDto)
		{
			var username = User.GetUsername();
			if (username.ToLower() == createMessageDto.RecipientUsername.ToLower())
			{
				return BadRequest("You cannot send message to yourself");
			}
			var sender = await _userRepository.FindUserByNameAsync(username);
			var recipient = await _userRepository.FindUserByNameAsync(createMessageDto.RecipientUsername);

			if (recipient == null || sender == null) return BadRequest("Cannot send message(some value is null)");

			var message = new Message
			{
				Sender = sender,
				Recipient = recipient,
				SenderUsername = sender.UserName,
				RecipientUsername = recipient.UserName,
				Content = createMessageDto.Content
			};

			_messageRepository.AddMessage(message);

			if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message)+ $"wysyłający: {username}, odbierający {recipient.UserName}");

			return BadRequest("Failed to save message");


		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
		{
			messageParams.Username = User.GetUsername();

			var messages = await _messageRepository.GetMessagesForUser(messageParams);

			Response.AddPaginationHeader(messages);

			return messages;


		}

		[HttpGet("thread/{userName}")]
		public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string userName)
		{
			var currentUsername = User.GetUsername();
			return Ok(await _messageRepository.GetMessageThread(currentUsername, userName));
		}
	}
}