using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly DataContext _context;

		public MessageRepository(DataContext context)
		{
			_context = context;
		}

		public void AddMessage(Message message)
		{
			_context.Messages.Add(message);
		}

		public void DeleteMessage(Message message)
		{
			_context.Messages.Remove(message);
		}

		public async Task<Message> GetMessage(Guid id)
		{
			return await _context.Messages.FindAsync(id);
		}

		public async Task<PagedList<MessageDto>> GetMessagesForUser(UserParams userParams)
		{
			var query = _context.Messages
						.OrderByDescending(m => m.MessageSent)
						.Select(m => new MessageDto
						{
							Id = m.Id,
							SenderId = m.SenderId,
							SenderUsername = m.SenderUsername,
							RecipientId = m.RecipientId,
							RecipientUsername = m.RecipientUsername,
							Content = m.Content,
							DateRead = m.DateRead,
							MessageSent = m.MessageSent
						}).AsQueryable();

			return await PagedList<MessageDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
		{
			var messages = await _context.Messages
						.Where(m => (m.RecipientUsername == currentUsername && m.SenderUsername == recipientUsername) ||
									(m.RecipientUsername == recipientUsername && m.SenderUsername == currentUsername))
						.OrderBy(m => m.MessageSent)
						.Select(m => new MessageDto
						{
							Id = m.Id,
							SenderUsername = m.SenderUsername,
							RecipientUsername = m.RecipientUsername,
							Content = m.Content,
							DateRead = m.DateRead,
							MessageSent = m.MessageSent
						})
						.ToListAsync();
			
			var unreadMessages = messages.Where(x => x.DateRead == null&& x.RecipientUsername == currentUsername ).ToList();
			
			if(unreadMessages.Count !=0 )
			{
				unreadMessages.ForEach(x => x.DateRead = DateTime.UtcNow);
				await _context.SaveChangesAsync();
			}
			
			return messages;
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}