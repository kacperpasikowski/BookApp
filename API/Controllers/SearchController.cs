using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.helpers;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SearchController : ControllerBase
	{
		private readonly ISearchService _searchService;
		
		public SearchController(ISearchService searchService)
		{
			_searchService = searchService;
		}
		
		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] UserParams userParams,[FromQuery] string query)
		{
			var results = await _searchService.SearchAsync(userParams, query);
			return Ok(results);
		}
	}
}