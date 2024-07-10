using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class PublisherService : IPublisherService
	{
		private readonly IPublisherRepository _publisherRepository;

		public PublisherService(IPublisherRepository publisherRepository)
		{
			_publisherRepository = publisherRepository;
		}
		public async Task<IEnumerable<GetPublisherDto>> GetAllPublishersAsync()
		{
			var publishers = await _publisherRepository.GetAllPublishersAsync();
			return publishers.Select(publisher => new GetPublisherDto
			{
				Id = publisher.Id,
				Name = publisher.Name,
				Books = publisher.Books.Select(book => new PublisherBooksDto
				{
					Id = book.Id,
					Title = book.Title,
					DateOfPublish = book.DateOfPublish
				}).ToList()
			}).ToList();
		}

		public async Task<GetPublisherDto> GetPublisherByIdAsync(Guid id)
		{
			var publisher = await _publisherRepository.GetPublisherByIdAsync(id);

			if (publisher == null)
			{
				return null;
			}

			return new GetPublisherDto
			{
				Id = publisher.Id,
				Name = publisher.Name,
				Books = publisher.Books.Select(book => new PublisherBooksDto
				{
					Id = book.Id,
					Title = book.Title,
					DateOfPublish = book.DateOfPublish
				}).ToList()
			};
		}
		public async Task<PublisherDto> AddPublisherAsync(PublisherDto publisherDto)
		{
			var publisher = new Publisher
			{
				Id = Guid.NewGuid(),
				Name = publisherDto.Name,
				Books = new List<Book>()
			};

			await _publisherRepository.AddPublisherAsync(publisher);

			return new PublisherDto
			{
				Name = publisher.Name
			};
		}

		public async Task<bool> UpdatePublisherAsync(Guid id, PublisherDto publisherDto)
		{
			var existingPublisher = await _publisherRepository.GetPublisherByIdAsync(id);
			if (existingPublisher == null)
			{
				return false;
			}

			existingPublisher.Name = publisherDto.Name;
			await _publisherRepository.UpdatePublisherAsync(existingPublisher);
			return true;
		}

		public async Task<bool> DeletePublisherAsync(Guid id)
		{
			var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
			if (publisher == null)
			{
				return false;
			}

			await _publisherRepository.DeletePublisherAsync(id);
			return true;
		}
	}
}