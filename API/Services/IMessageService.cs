using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.helpers;

namespace API.Services
{
	public interface IMessageService
	{
		Task AddMessageAsync(Guid senderId, Guid recipientId, string content);
		Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
	}
}