using API.Data;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly DataContext _context;
		
		public PublisherRepository(DataContext context)
		{
			_context = context;
		}
		
		public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
		{
			return await _context.Publishers
				.Include(p=> p.Books)
				.ToListAsync();
		}

		public async Task<Publisher> GetPublisherByIdAsync(Guid id)
		{
			return await _context.Publishers
            .Include(p => p.Books)
            .FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Publisher> AddPublisherAsync(Publisher publisher)
		{
			_context.Publishers.Add(publisher);
			await _context.SaveChangesAsync();
			return publisher;
		}
		public async Task<Publisher> UpdatePublisherAsync(Publisher publisher)
		{
			_context.Publishers.Update(publisher);
			await _context.SaveChangesAsync();
			return publisher;
		}

		public async Task<Publisher> DeletePublisherAsync(Guid id)
		{
			var publisher = await _context.Publishers.FindAsync(id);
			if(publisher == null)
			{
				throw new KeyNotFoundException("Publisher was not found");
			}
			_context.Publishers.Remove(publisher);
			await _context.SaveChangesAsync();
			return publisher;
		}

		

		
	}
}