using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.DTOs;
using API.helpers;
using API.Repositories;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Services
{
	public class SearchService : ISearchService
	{
		
		private readonly IBookRepository _bookRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IAuthorService _authorService;
		private readonly IMapper _mapper;

		public SearchService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, 
		IAuthorService authorService)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
			_mapper = mapper;
			_authorService = authorService;
		}

		public async Task<PagedList<SearchResultDto>> SearchAsync(UserParams userParams, string query)
		{
			var pagedBooks = await _bookRepository.GetAllBooksAsync(userParams);
			var pagedAuthors = await _authorRepository.GetAllAuthorsAsync(userParams);
			
			query = query.ToLower();
			
			var filteredBooks = pagedBooks.Where(book => 
			book.Title.ToLower().Contains(query));
			
			var filderedAuthors = pagedAuthors.Where(author =>
			author.Name.ToLower().Contains(query));
			
			var bookResults = _mapper.Map<IEnumerable<SearchResultDto>>(filteredBooks);
			
			var authorResults = new List<SearchResultDto>();
			foreach(var author in filderedAuthors)
			{
				var authorDto = _mapper.Map<SearchResultDto>(author);
				authorDto.MainCategory = await _authorService.GetAuthorMainCategory(author.Id);
				authorResults.Add(authorDto);
			}
			
			var combinedResults = bookResults.Concat(authorResults).ToList();
			
			var pagedresults = combinedResults
				.Skip((userParams.PageNumber -1) * userParams.PageSize)
				.Take(userParams.PageSize)
				.ToList();
			
			return new PagedList<SearchResultDto>(pagedresults, combinedResults.Count, userParams.PageNumber, userParams.PageSize);
			
			
		}
	}
}