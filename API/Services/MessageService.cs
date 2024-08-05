using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class MessageService : IMessageService
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IUserRepository _userRepository;

		public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
		{
			_messageRepository = messageRepository;
			_userRepository = userRepository;
		}

		public async Task AddMessageAsync(Guid senderId, Guid recipientId, string content)
		{
			
			if(senderId == recipientId)
			{
				throw new ArgumentException("you can't send a message to yourself!");
			}
			
			var sender = await _userRepository.FindUserByIdAsync(senderId);
			var recipient = await _userRepository.FindUserByIdAsync(recipientId);
			
			if(sender == null || recipient == null)
			{
				throw new ArgumentException("Sender or recipient not found : C");
			}
			
			var message = new Message
			{
				SenderId = senderId,
				SenderUsername = sender.UserName,
				RecipientId = recipientId,
				RecipientUsername = recipient.UserName,
				Content = content,
				MessageSent = DateTime.UtcNow
				
			};
			
			_messageRepository.AddMessage(message);
			await _messageRepository.SaveAllAsync();
		}

		public async Task<PagedList<MessageDto>> GetMessagesForUser(UserParams userParams)
		{
			return await _messageRepository.GetMessagesForUser(userParams);
		}

		public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
		{
			return await _messageRepository.GetMessageThread(currentUsername, recipientUsername);
		}
	}
}