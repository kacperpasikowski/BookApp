using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
	public interface IPublisherService
	{
		Task<IEnumerable<GetPublisherDto>> GetAllPublishersAsync();
		Task<GetPublisherDto> GetPublisherByIdAsync(Guid id);
		Task<PublisherDto> AddPublisherAsync(PublisherDto publisherDto);
		Task<bool> UpdatePublisherAsync(Guid id, PublisherDto publisherDto);
		Task<bool> DeletePublisherAsync(Guid id);
		
	}
}