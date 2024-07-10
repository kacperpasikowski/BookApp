import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BookSummary } from '../models/book-summary.model';
import { BookDetail } from '../models/book-detail.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = "https://localhost:5001/api/book";

  constructor(private http: HttpClient) { }



  getBooks(): Observable<BookSummary[]> {
    return this.http.get<BookSummary[]>(this.apiUrl).pipe(
      map((books: any[]) => books.map(book => ({
        id: book.id,
        title: book.title,
        dateOfPublish: book.dateOfPublish,
        bookAvatarUrl: book.bookAvatarUrl,
        authors: book.authors.map((author: { name: string }) => author.name) 
      })))
    );
  }

  getBook(id: string): Observable<BookDetail>{
    return this.http.get<BookDetail>(`${this.apiUrl}/${id}`);
  }
}
