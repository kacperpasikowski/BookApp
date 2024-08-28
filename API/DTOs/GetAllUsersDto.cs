using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
	public class GetAllUsersDto
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string UserAvatarUrl { get; set; }
		public List<string> Roles { get; set; }
		public List<ReadBooksDto> ReadBooks { get; set; }
		public List<FavoriteAuthorsDto> FavoriteAuthors { get; set; }
		public List<FriendDto> Friends { get; set; }
		
		
	}
}