using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;

namespace API.Repositories.Interfaces
{
	public interface IMessageRepository
	{
		void AddMessage(Message message);
		void DeleteMessage(Message message);
		Task<Message> GetMessage(Guid id);
		Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
		Task<PagedList<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername, PaginationParams paginationParams);
		Task<bool> SaveAllAsync();
	}
}