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
		private readonly IMessageService _messageService;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IMessageRepository _messageRepository;

		public MessageController(IMessageService messageService, IMapper mapper, IUserRepository userRepository, IMessageRepository messageRepository)
		{
			_messageService = messageService;
			_mapper = mapper;
			_userRepository = userRepository;
			_messageRepository = messageRepository;
		}
		
		[HttpPost]
		public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto createMessageDto)
		{
			var username = User.GetUsername();
			if(username== createMessageDto.RecipientUsername.ToLower())
			{
				return BadRequest("You cannot send message to yourself");
			}
			var sender = await _userRepository.FindUserByNameAsync(username);
			var recipient = await _userRepository.FindUserByNameAsync(createMessageDto.RecipientUsername);
			
			if(recipient==null || sender == null) return BadRequest("Cannot send message(some value is null)");
			
			var message = new Message
			{
				Sender = sender,
				Recipient = recipient,
				SenderUsername = sender.UserName,
				RecipientUsername = recipient.UserName,
				Content = createMessageDto.Content
			};
			
			_messageRepository.AddMessage(message);
			
			if(await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));
			
			return BadRequest("Failed to save message");
			
			
		}
		[HttpGet]
		public async Task<IActionResult> GetMessagesForUser([FromQuery] UserParams userParams)
		{
			var messages = await _messageService.GetMessagesForUser(userParams);
			return Ok(messages);
		}
		
		[HttpGet("thread")]
		public async Task<IActionResult> GetMessageThread([FromQuery] string currentUsername, [FromQuery] string recipientUsername )
		{
			var messages = await _messageService.GetMessageThread(currentUsername, recipientUsername);
			return Ok(messages);
		}
	}
}