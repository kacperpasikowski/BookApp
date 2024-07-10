

using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PublisherController : ControllerBase
	{
		private readonly IPublisherService _publisherService;


		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPublishers()
		{
			var publishers = await _publisherService.GetAllPublishersAsync();
			return Ok(publishers);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPublisherById(Guid id)
		{
			var publisher = await _publisherService.GetPublisherByIdAsync(id);
			if (publisher == null)
			{
				return NotFound();
			}
			return Ok(publisher);
		}
		[HttpPost]
		public async Task<IActionResult> AddPublisher([FromBody] PublisherDto publisherDto)
		{
			if (publisherDto == null)
			{
				return BadRequest();
			}

			var newPublisher = await _publisherService.AddPublisherAsync(publisherDto);
			return CreatedAtAction(nameof(GetPublisherById), new { id = newPublisher.Id}, newPublisher);
			
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePublisher(Guid id, [FromBody] PublisherDto publisherDto)
		{
			if (publisherDto == null)
			{
				return BadRequest();
			}
			var success = await _publisherService.UpdatePublisherAsync(id, publisherDto);
			if(!success)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePublisher(Guid id)
		{
			var success = await _publisherService.DeletePublisherAsync(id);
			if (!success)
			{
				return NotFound();
			}
			await _publisherService.DeletePublisherAsync(id);
			return NoContent();
		}

	}
}