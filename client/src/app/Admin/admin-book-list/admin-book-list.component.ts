import { Component, inject, OnInit } from '@angular/core';
import { AdminBookService } from '../AdminServices/admin-book.service';
import { BookService } from 'src/app/services/book.service';
import { BookDetail } from 'src/app/models/book-detail.model';
import { AuthorService } from 'src/app/services/author.service';
import { CategoryService } from 'src/app/services/category.service';
import { PublisherService } from 'src/app/services/publisher.service';
import { Category } from 'src/app/models/category.model';
import { Author } from 'src/app/models/author.model';
import { Publisher } from 'src/app/models/publisher.model';
import { UserParams } from 'src/app/models/userParams';

@Component({
  selector: 'app-admin-book-list',
  templateUrl: './admin-book-list.component.html',
  styleUrls: ['./admin-book-list.component.css']
})
export class AdminBookListComponent implements OnInit {
  private adminBookService = inject(AdminBookService);
  bookService = inject(BookService);
  private authorService = inject(AuthorService);
  private categoryService = inject(CategoryService);
  private publisherService = inject(PublisherService);

  books: BookDetail[] = [];
  categories: Category[] = [];
  authors: Author[] = [];
  publishers: Publisher[] = [];
  editingBookId: string | null = null;
  userParams = new UserParams();

  ngOnInit(): void {
    this.loadBooks();
    this.loadAuthors();
    this.loadCategories();
    this.loadPublishers();
  }

  loadBooks() {
    this.bookService.getBooks(this.userParams)
  }

  deleteBook(id: string) {
    if (window.confirm('Are you sure you want to delete this book?')) {
      this.adminBookService.deleteBook(id).subscribe({
        next: () => {
          console.log("Book has been deleted");
          this.loadBooks();
        },
        error: error => console.log(error)
      });
    }
  }

  editBook(book: BookDetail) {
    this.editingBookId = book.id;
    this.books.forEach(b => b.editing = false);
    book.editing = true;
    console.log('Editing book:', book);
  }

  cancelEdit(book: BookDetail) {
    book.editing = false;
    this.editingBookId = null;
  }

  updateBook(book: BookDetail) {
    console.log('Form value on submit:', book);
    if (this.editingBookId && this.isValid(book)) {
      this.adminBookService.updateBook(this.editingBookId, book).subscribe({
        next: () => {
          console.log(`Book with ID: ${this.editingBookId} has been updated`);
          book.editing = false;
          this.editingBookId = null;
          this.loadBooks();
        },
        error: error => console.log(error)
      });
    } else {
      console.log('Form is invalid');
    }
  }

  isValid(book: BookDetail): boolean {
    return book.title !== '' && book.description !== '' && book.dateOfPublish !== '' &&
      book.publisherName !== '' && book.bookAvatarUrl !== '' && book.authors.length > 0 &&
      book.categories.length > 0;
  }

  loadAuthors() {
    this.authorService.getAuthors().subscribe({
      next: authors => this.authors = authors,
      error: error => console.log(error)
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: categories => this.categories = categories,
      error: error => console.log(error)
    });
  }

  loadPublishers() {
    this.publisherService.getPublishers().subscribe({
      next: publishers => {
        this.publishers = publishers;
        console.log('Publishers loaded:', this.publishers);
      },
      error: error => console.log(error)
    });
  }

  pageChanged(event: any){
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadBooks();
    }
  }
}
