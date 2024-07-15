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
    return this.http.get<BookSummary[]>(this.apiUrl);
  }

  getBook(id: string): Observable<BookDetail>{
    return this.http.get<BookDetail>(`${this.apiUrl}/${id}`);
  }
}
