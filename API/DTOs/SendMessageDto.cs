using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class SendMessageDto
	{
		public Guid SenderId { get; set; }
		public Guid RecipientId { get; set; }
		public string Content { get; set; }
	}
}