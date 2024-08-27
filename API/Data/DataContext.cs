using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookAuthor> BookAuthors { get; set; }
		public DbSet<BookCategory> BookCategories { get; set; }
		public DbSet<BookGrade> BookGrades { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<UserBook> UserBooks { get; set; }
		public DbSet<UserFavoriteAuthor> UserFavoriteAuthors { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Friend> Friends { get; set; }
		public DbSet<FriendRequest> FriendRequests { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			SeedRoles(builder);


			//Publisher Book 1:N
			builder.Entity<Book>()
				.HasOne(b => b.Publisher)
				.WithMany(p => p.Books)
				.HasForeignKey(b => b.PublisherId);

			//Book - AppUser N:M Grades	
			builder.Entity<BookGrade>()
				.HasKey(bg => new { bg.UserId, bg.BookId });

			builder.Entity<BookGrade>()
				.HasOne(bg => bg.AppUser)
				.WithMany(u => u.BookGrades)
				.HasForeignKey(bg => bg.UserId);

			builder.Entity<BookGrade>()
				.HasOne(bg => bg.Book)
				.WithMany(b => b.BookGrades)
				.HasForeignKey(bg => bg.BookId);

			//Book - AppUser N:M ReadBooks
			builder.Entity<UserBook>()
				.HasKey(ub => new { ub.UserId, ub.BookId });

			builder.Entity<UserBook>()
				.HasOne(ub => ub.AppUser)
				.WithMany(u => u.BooksRead)
				.HasForeignKey(ub => ub.UserId);

			builder.Entity<UserBook>()
				.HasOne(ub => ub.Book)
				.WithMany(b => b.UserBooks)
				.HasForeignKey(ub => ub.BookId);

			//User - Author N:M UserFavoriteAuthor
			builder.Entity<UserFavoriteAuthor>()
				.HasKey(ufa => new { ufa.UserId, ufa.AuthorId });

			builder.Entity<UserFavoriteAuthor>()
				.HasOne(ufa => ufa.AppUser)
				.WithMany(u => u.UserFavoriteAuthors)
				.HasForeignKey(ufa => ufa.UserId);

			builder.Entity<UserFavoriteAuthor>()
				.HasOne(ufa => ufa.Author)
				.WithMany(a => a.UserFavoriteAuthors)
				.HasForeignKey(ufa => ufa.AuthorId);

			//Book - Author N:M BookAuthor
			builder.Entity<BookAuthor>()
				.HasKey(ba => new { ba.BookId, ba.AuthorId });

			builder.Entity<BookAuthor>()
				.HasOne(ba => ba.Book)
				.WithMany(b => b.BookAuthors)
				.HasForeignKey(ba => ba.BookId);

			builder.Entity<BookAuthor>()
				.HasOne(ba => ba.Author)
				.WithMany(a => a.BookAuthors)
				.HasForeignKey(ba => ba.AuthorId);

			//Book - Category N:M BookCategory
			builder.Entity<BookCategory>()
				.HasKey(bc => new { bc.BookId, bc.CategoryId });

			builder.Entity<BookCategory>()
				.HasOne(bc => bc.Book)
				.WithMany(b => b.BookCategories)
				.HasForeignKey(bc => bc.BookId);

			builder.Entity<BookCategory>()
				.HasOne(bc => bc.Category)
				.WithMany(c => c.BookCategories)
				.HasForeignKey(bc => bc.CategoryId);

			//Message
			builder.Entity<Message>()
				.HasOne(x => x.Recipient)
				.WithMany(x => x.MessagesReceived)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Message>()
				.HasOne(x => x.Sender)
				.WithMany(x => x.MessagesSent)
				.OnDelete(DeleteBehavior.Restrict);
				
			//Friends
			builder.Entity<Friend>()
				.HasKey(f => new {f.UserId1, f.UserId2});
			
			builder.Entity<Friend>()
				.HasOne(f => f.User1)
				.WithMany(u => u.Friends)
				.HasForeignKey(f => f.UserId1)
				.OnDelete(DeleteBehavior.Restrict);
				
			builder.Entity<Friend>()
				.HasOne(f => f.User2)
				.WithMany()
				.HasForeignKey(f => f.UserId2)
				.OnDelete(DeleteBehavior.Restrict);
				
			//FriendRequests
			builder.Entity<FriendRequest>()
				.HasOne(fr => fr.Requester)
				.WithMany(u => u.SentFriendRequest)
				.HasForeignKey(fr => fr.FromUserId)
				.OnDelete(DeleteBehavior.Restrict);
				
			builder.Entity<FriendRequest>()
				.HasOne(fr => fr.Receiver)
				.WithMany(u => u.ReceivedFriendRequest)
				.HasForeignKey(fr => fr.ToUserId)
				.OnDelete(DeleteBehavior.Restrict);

		}
		private void SeedRoles(ModelBuilder builder)
		{
			var roles = new List<IdentityRole<Guid>>
		{
			new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Moderator", NormalizedName = "MODERATOR" },
			new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" }
		};

			builder.Entity<IdentityRole<Guid>>().HasData(roles);
		}

	}
}