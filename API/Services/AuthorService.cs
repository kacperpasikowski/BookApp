using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
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
		
		public async Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync()
		{
			var authors = await _authorRepository.GetAllAuthorsAsync();
			return authors.Select(author => new GetAuthorDto
			{
				Id = author.Id,
				Name = author.Name,
				AuthorAvatarUrl = author.AuthorAvatarUrl,
				DateOfBirth = author.DateOfBirth,
				Books = author.BookAuthors.Select(ba => new AuthorsBookDto
				{
					Id = ba.Book.Id,
					Title = ba.Book.Title,
					DateOfPublish = ba.Book.DateOfPublish
				}).ToList()
			});
			
			
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
					DateOfPublish = ba.Book.DateOfPublish
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