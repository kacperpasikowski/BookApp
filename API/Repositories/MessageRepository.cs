using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public MessageRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
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

		public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
		{
			var query = _context.Messages
						.OrderByDescending(m => m.MessageSent)
						.AsQueryable();

			query = messageParams.Container switch
			{
				"Inbox" => query.Where(x => x.Recipient.UserName == messageParams.Username),
				"Outbox" => query.Where(x => x.Sender.UserName == messageParams.Username),
				_ => query.Where(x => x.Recipient.UserName == messageParams.Username && x.DateRead == null)
			};
			
			var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);
			
			return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
			
		}

		public async Task<PagedList<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername, PaginationParams paginationParams)
		{
			var messages = _context.Messages
						.Where(m => (m.RecipientUsername == currentUsername && m.SenderUsername == recipientUsername) ||
									(m.RecipientUsername == recipientUsername && m.SenderUsername == currentUsername))
						.OrderByDescending(m => m.MessageSent)
						.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
						.AsQueryable();
			
			var unreadMessages = messages.Where(x => x.DateRead == null&& x.RecipientUsername == currentUsername ).ToList();
			
			if(unreadMessages.Count !=0 )
			{
				unreadMessages.ForEach(x => x.DateRead = DateTime.UtcNow);
				await _context.SaveChangesAsync();
			}
			
			return await PagedList<MessageDto>.CreateAsync(messages, paginationParams.PageNumber,paginationParams.PageSize);
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}