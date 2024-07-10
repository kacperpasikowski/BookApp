import { Component, OnInit } from '@angular/core';
import { BookSummary } from '../models/book-summary.model';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit{
  books: BookSummary[] = [];

  constructor(private bookService: BookService){}

  ngOnInit(): void {
    this.bookService.getBooks().subscribe(data => {
      this.books = data;
    })
  }

}
