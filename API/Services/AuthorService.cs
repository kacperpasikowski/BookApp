using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;
using AutoMapper;

namespace API.Services
{
	public class AuthorService : IAuthorService
	{

		private readonly IAuthorRepository _authorRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public AuthorService(IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IMapper mapper)
		{
			_authorRepository = authorRepository;
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}
		public async Task<string> GetAuthorMainCategory(Guid id)
		{
			var author = await _authorRepository.GetAuthorByIdAsync(id);
			if (author == null)
			{
				return null;
			}

			var categoryCount = new Dictionary<Guid, int>();

			foreach (var bookAuthor in author.BookAuthors)
			{
				foreach (var bookCategory in bookAuthor.Book.BookCategories)
				{
					var categoryId = bookCategory.CategoryId;
					if (categoryCount.ContainsKey(categoryId))
					{
						categoryCount[categoryId]++;
					}
					else
					{
						categoryCount[categoryId] = 1;
					}
				}
			}

			var mainCategoryId = categoryCount.OrderByDescending(c => c.Value).FirstOrDefault().Key;

			var mainCategory = await _categoryRepository.GetCategoryByIdAsync(mainCategoryId);

			if (mainCategory == null)
			{
				return null;
			}

			return mainCategory.Name;


		}
		public async Task<PagedList<GetAuthorDto>> GetAllAuthorsAsync(UserParams userParams)
		{
			var pagedAuthors = await _authorRepository.GetAllAuthorsAsync(userParams);

			var authorsDtos = new List<GetAuthorDto>();


			foreach (var author in pagedAuthors)
			{
				var mainCategory = await GetAuthorMainCategory(author.Id);
				var authorDto = new GetAuthorDto
				{
					Id = author.Id,
					Name = author.Name,
					AuthorAvatarUrl = author.AuthorAvatarUrl,
					DateOfBirth = author.DateOfBirth,
					MainCategory = mainCategory,
					Books = author.BookAuthors.Select(ba => new AuthorsBookDto
					{
						Id = ba.Book.Id,
						Title = ba.Book.Title,
						DateOfPublish = ba.Book.DateOfPublish,
						BookAvatarUrl = ba.Book.BookAvatarUrl,
						CategoryName = ba.Book.BookCategories.FirstOrDefault()?.Category.Name
					}).ToList()
				};
				authorsDtos.Add(authorDto);
			}


			return new PagedList<GetAuthorDto>(authorsDtos, pagedAuthors.TotalCount, userParams.PageNumber, userParams.PageSize);
		}

		public async Task<GetAuthorDto> GetAuthorByIdAsync(Guid id)
		{
			var author = await _authorRepository.GetAuthorByIdAsync(id);



			if (author == null)
			{
				return null;
			}

			var mainCategory = await GetAuthorMainCategory(id);

			return new GetAuthorDto
			{
				Id = author.Id,
				Name = author.Name,
				AuthorAvatarUrl = author.AuthorAvatarUrl,
				MainCategory = mainCategory,
				DateOfBirth = author.DateOfBirth,
				Books = author.BookAuthors.Select(ba => new AuthorsBookDto
				{
					Id = ba.Book.Id,
					Title = ba.Book.Title,
					DateOfPublish = ba.Book.DateOfPublish,
					BookAvatarUrl = ba.Book.BookAvatarUrl,
					CategoryName = ba.Book.BookCategories.FirstOrDefault()?.Category.Name
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