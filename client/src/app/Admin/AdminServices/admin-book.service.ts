import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookDetail } from 'src/app/models/book-detail.model';

@Injectable({
  providedIn: 'root'
})
export class AdminBookService {
  private apiUrl = "https://localhost:5001/api/book";

  constructor(private http: HttpClient) { }

  addBook(book:any): Observable<BookDetail>{
    return this.http.post<BookDetail>(this.apiUrl, book);
  }
  deleteBook(id:string){
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  updateBook(id: string, book: BookDetail){
    return this.http.put<BookDetail>(`${this.apiUrl}/${id}`, book)
  }
}
