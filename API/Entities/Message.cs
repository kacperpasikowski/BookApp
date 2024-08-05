using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class Message
	{
		public Guid Id { get; set; }
		public string SenderUsername { get; set; }
		public string RecipientUsername { get; set; }
		public string Content { get; set; }
		public DateTime? DateRead { get; set; }
		public DateTime MessageSent { get; set; } = DateTime.UtcNow;
		public bool SenderDeleted { get; set; }
		public bool RecipientDeleted { get; set; }
		
		//navigation properties
		public Guid SenderId { get; set; }
		public AppUser Sender { get; set; } = null!;
		public Guid RecipientId { get; set; }
		public AppUser Recipient { get; set; } = null!;
		
	}
}