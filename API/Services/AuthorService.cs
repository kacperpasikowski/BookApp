using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class AuthorService : IAuthorService
	{
		
		private readonly IAuthorRepository _authorRepository;
		
		public AuthorService(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}
		
		public async Task<PagedList<GetAuthorDto>> GetAllAuthorsAsync(UserParams userParams)
		{
			var pagedAuthors = await _authorRepository.GetAllAuthorsAsync(userParams);
			
			var authorsDto = pagedAuthors.Select(author => new GetAuthorDto
			{
				Id = author.Id,
				Name = author.Name,
				AuthorAvatarUrl = author.AuthorAvatarUrl,
				DateOfBirth = author.DateOfBirth,
				Books = author.BookAuthors.Select(ba => new AuthorsBookDto
				{
					Id = ba.Book.Id,
					Title = ba.Book.Title,
					DateOfPublish = ba.Book.DateOfPublish,
					BookAvatarUrl = ba.Book.BookAvatarUrl
				}).ToList()
			});
			
			return new PagedList<GetAuthorDto>(authorsDto, pagedAuthors.TotalCount, userParams.PageNumber, userParams.PageSize );
		}

		public async Task<GetAuthorDto> GetAuthorByIdAsync(Guid id)
		{
			var author = await _authorRepository.GetAuthorByIdAsync(id);
			if (author == null)
			{
				return null;
			}
			return new GetAuthorDto
			{
				Id = author.Id,
				Name = author.Name,
				AuthorAvatarUrl = author.AuthorAvatarUrl,
				DateOfBirth = author.DateOfBirth,
				Books = author.BookAuthors.Select(ba => new AuthorsBookDto
				{
					Id = ba.Book.Id,
					Title = ba.Book.Title,
					DateOfPublish = ba.Book.DateOfPublish,
					BookAvatarUrl = ba.Book.BookAvatarUrl
				}).ToList()
			};
		}
		public async Task<AddAuthorDto> AddAuthorAsync(AddAuthorDto addAuthorDto)
		{
			var author = new Author
			{
				Id = Guid.NewGuid(),
				Name = addAuthorDto.Name,
				AuthorAvatarUrl = addAuthorDto.AuthorAvatarUrl,
				DateOfBirth = addAuthorDto.DateOfBirth,
				BookAuthors = new List<BookAuthor>(),
				UserFavoriteAuthors = new List<UserFavoriteAuthor>()
			};
			
			
			
			await _authorRepository.AddAuthorAsync(author);
			
			addAuthorDto.Id = author.Id;
			
			return addAuthorDto;
		}

		public async Task<bool> DeleteAuthorAsync(Guid id)
		{
			var author = await _authorRepository.GetAuthorByIdAsync(id);
			if (author == null)
			{
				return false;
			}
			
			await _authorRepository.DeleteAuthorAsync(id);
			return true;
		}
	}
}