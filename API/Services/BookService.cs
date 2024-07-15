using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
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

		public async Task<IEnumerable<GetBookDto>> GetAllBooksAsync()
		{
			var books = await _bookRepository.GetAllBooksAsync();

			return books.Select(book => new GetBookDto
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

		public async Task<bool> DeleteBookAsync(Guid id)
		{
			var book = await _bookRepository.GetBookByIdAsync(id);
			if(book == null)
			{
				return false;
			}
			await _bookRepository.DeleteBookAsync(id);
			return true;
		}
	}
}