using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Repositories.Interfaces;

namespace API.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IPublisherRepository _publisherRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly ICategoryRepository _categoryRepository;


		public BookService(IBookRepository bookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
		{
			_bookRepository = bookRepository;
			_publisherRepository = publisherRepository;
			_authorRepository = authorRepository;
			_categoryRepository = categoryRepository;
		}

		public async Task<PagedList<GetBookDto>> SearchBooksAsync(UserParams userParams, string query)
		{
			var pagedBooks = await _bookRepository.GetAllBooksAsync(userParams);
			
			query = query.ToLower();
			
			var filteredBooks = pagedBooks.Where(book =>
				book.Title.ToLower().Contains(query) ||
				book.BookAuthors.Any(ba => ba.Author.Name.Contains(query))
			);

			var bookDtos = filteredBooks.Select(book => new GetBookDto
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				DateOfPublish = book.DateOfPublish,
				PublisherName = book.Publisher.Name,
				BookAvatarUrl = book.BookAvatarUrl,
				Authors = book.BookAuthors.Select(ba => new BookAuthorsDto
				{
					Id = ba.Author.Id,
					Name = ba.Author.Name,

				}).ToList(),
				Categories = book.BookCategories.Select(bc => new CategoryDto
				{
					Id = bc.Category.Id,
					Name = bc.Category.Name
				}).ToList()
			}).ToList();

			return new PagedList<GetBookDto>(bookDtos, pagedBooks.TotalCount, userParams.PageNumber, userParams.PageSize);

		}

		public async Task<PagedList<GetBookDto>> GetAllBooksAsync(UserParams userParams)
		{
			var pagedBooks = await _bookRepository.GetAllBooksAsync(userParams);

			var bookDtos = pagedBooks.Select(book => new GetBookDto
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				DateOfPublish = book.DateOfPublish,
				PublisherName = book.Publisher.Name,
				BookAvatarUrl = book.BookAvatarUrl,
				CategoryName = book.BookCategories.FirstOrDefault().Category.Name,
				Authors = book.BookAuthors.Select(ba => new BookAuthorsDto
				{
					Id = ba.Author.Id,
					Name = ba.Author.Name,

				}).ToList(),
				Categories = book.BookCategories.Select(bc => new CategoryDto
				{
					Id = bc.Category.Id,
					Name = bc.Category.Name
				}).ToList(),
				AverageGrade = CalculateAverageGrade(book.Id).Result
			}).ToList();

			return new PagedList<GetBookDto>(bookDtos, pagedBooks.TotalCount, userParams.PageNumber, userParams.PageSize);

		}

		public async Task<GetBookDto> GetBookByIdAsync(Guid id)
		{
			var book = await _bookRepository.GetBookByIdAsync(id);

			if (book == null)
			{
				return null;
			}

			return new GetBookDto
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				DateOfPublish = book.DateOfPublish,
				PublisherName = book.Publisher.Name,
				BookAvatarUrl = book.BookAvatarUrl,
				CategoryName = book.BookCategories.FirstOrDefault().Category.Name,
				Authors = book.BookAuthors.Select(ba => new BookAuthorsDto
				{
					Id = ba.Author.Id,
					Name = ba.Author.Name,

				}).ToList(),
				Categories = book.BookCategories.Select(bc => new CategoryDto
				{
					Id = bc.Category.Id,
					Name = bc.Category.Name
				}).ToList(),
				AverageGrade = await CalculateAverageGrade(book.Id)
				
			};

		}


		public async Task<GetBookDto> AddBookAsync(AddBookDto addBookDto)
		{
			var publisher = await _publisherRepository.GetPublisherByIdAsync(addBookDto.PublisherId);

			if (publisher == null)
			{
				throw new ArgumentException("publisher not found");
			}

			var authors = new List<Author>();

			foreach (var authorId in addBookDto.AuthorIds)
			{
				var author = await _authorRepository.GetAuthorByIdAsync(authorId);
				if (author != null)
				{
					authors.Add(author);
				}
			}

			var categories = new List<Category>();

			foreach (var categoryId in addBookDto.CategoryIds)
			{
				var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
				if (category != null)
				{
					categories.Add(category);
				}
			}
			var book = new Book
			{
				Id = Guid.NewGuid(),
				Title = addBookDto.Title,
				Description = addBookDto.Description,
				DateOfPublish = addBookDto.DateOfPublish,
				PublisherId = addBookDto.PublisherId,
				BookAvatarUrl = addBookDto.BookAvatarUrl,
				BookAuthors = authors.Select(a => new BookAuthor { AuthorId = a.Id }).ToList(),
				BookCategories = categories.Select(c => new BookCategory { CategoryId = c.Id }).ToList()
			};

			await _bookRepository.AddBookAsync(book);

			var bookDto = new GetBookDto
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				DateOfPublish = book.DateOfPublish,
				PublisherName = book.Publisher.Name,
				BookAvatarUrl = book.BookAvatarUrl,
				Authors = book.BookAuthors.Select(ba => new BookAuthorsDto
				{
					Id = ba.Author.Id,
					Name = ba.Author.Name,

				}).ToList(),
				Categories = book.BookCategories.Select(bc => new CategoryDto
				{
					Id = bc.Category.Id,
					Name = bc.Category.Name
				}).ToList()
			};

			return bookDto;
		}

		public async Task<GetBookDto> UpdateBookAsync(Guid bookId, AddBookDto addBookDto)
		{
			var book = await _bookRepository.GetBookByIdAsync(bookId);
			if (book == null)
			{
				throw new ArgumentException("Book was not found");
			}

			var publisher = await _publisherRepository.GetPublisherByIdAsync(addBookDto.PublisherId);
			if (publisher == null)
			{
				throw new ArgumentException("Publisher was not found");
			}

			var authors = new List<Author>();
			foreach (var authorId in addBookDto.AuthorIds)
			{
				var author = await _authorRepository.GetAuthorByIdAsync(authorId);
				if (author == null)
				{
					throw new ArgumentException($"Author with ID {authorId} was not found");
				}
				authors.Add(author);
			}

			var categories = new List<Category>();
			foreach (var categoryId in addBookDto.CategoryIds)
			{
				var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
				if (category == null)
				{
					throw new ArgumentException($"Category with ID {categoryId} was not found");
				}
				categories.Add(category);
			}

			book.Title = addBookDto.Title;
			book.Description = addBookDto.Description;
			book.DateOfPublish = addBookDto.DateOfPublish;
			book.BookAvatarUrl = addBookDto.BookAvatarUrl;
			book.PublisherId = addBookDto.PublisherId;
			book.BookAuthors = authors.Select(a => new BookAuthor { AuthorId = a.Id, BookId = book.Id }).ToList();
			book.BookCategories = categories.Select(c => new BookCategory { CategoryId = c.Id, BookId = book.Id }).ToList();

			await _bookRepository.UpdateBookAsync(book);

			var bookDto = new GetBookDto
			{
				Id = book.Id,
				Title = book.Title,
				Description = book.Description,
				DateOfPublish = book.DateOfPublish,
				PublisherName = publisher.Name,
				BookAvatarUrl = book.BookAvatarUrl,
				Authors = book.BookAuthors.Select(ba => new BookAuthorsDto
				{
					Id = ba.Author.Id,
					Name = ba.Author.Name,
				}).ToList(),
				Categories = book.BookCategories.Select(bc => new CategoryDto
				{
					Id = bc.Category.Id,
					Name = bc.Category.Name
				}).ToList()
			};

			return bookDto;
		}


		public async Task<bool> DeleteBookAsync(Guid id)
		{
			var book = await _bookRepository.GetBookByIdAsync(id);
			if (book == null)
			{
				return false;
			}
			await _bookRepository.DeleteBookAsync(id);
			return true;
		}
		
		private async Task<double> CalculateAverageGrade(Guid bookId)
		{
			var grades = await _bookRepository.GetBookGradesAsync(bookId);
			if (grades == null || !grades.Any())
			{
				return 0;
			}
			return grades.Average(bg => bg.Grade);
		}


	}
}