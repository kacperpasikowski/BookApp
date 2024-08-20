import { Component, inject, OnInit } from '@angular/core';
import { BookSummary } from '../models/book-summary.model';
import { BookService } from '../services/book.service';
import { BookDetail } from '../models/book-detail.model';
import { UserParams } from '../models/userParams';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit{
  books: BookSummary[] = [];
  bookService = inject(BookService);
  userParams = new UserParams();

  constructor(){}

  ngOnInit(): void {
    if (!this.bookService.paginatedResult()) this.loadBooks()
  }


  

  loadBooks(){
    this.bookService.getBooks(this.userParams)
  }


  pageChanged(event: any){
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadBooks();
    }
  }

  

}
