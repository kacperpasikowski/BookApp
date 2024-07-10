using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
		Task<Publisher> GetPublisherByIdAsync(Guid id);
		Task<Publisher> AddPublisherAsync(Publisher publisher);
		Task<Publisher> UpdatePublisherAsync(Publisher publisher);
		Task<Publisher> DeletePublisherAsync(Guid id);
		
    }
}