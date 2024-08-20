import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BookSummary } from '../models/book-summary.model';
import { BookDetail } from '../models/book-detail.model';
import { PaginatedResult } from '../models/pagination';
import { UserParams } from '../models/userParams';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = "https://localhost:5001/api/book";
  paginatedResult = signal<PaginatedResult<BookDetail[]>| null>(null);

  constructor(private http: HttpClient) { }



  getBooks(userParams: UserParams) {
    let params = this.setPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    return this.http.get<BookDetail[]>(this.apiUrl, {observe: 'response', params}).subscribe({
      next: response => {
        this.paginatedResult.set({
          items: response.body as BookDetail[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    });
  }

  private setPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    if(pageNumber && pageSize){
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize',pageSize);
    }

    return params;
  }

  getBook(id: string): Observable<BookDetail>{
    return this.http.get<BookDetail>(`${this.apiUrl}/${id}`);
  }

  // searchBooks(query: string, pageNumber: number = 1, pageSize: number = 10): Observable<PaginatedResult<BookDetail[]>> {
  //   let params = this.setPaginationHeaders(pageNumber, pageSize);
  //   params = params.append('query', query);
  
  //   return this.http.get<BookDetail[]>('https:localhost:5001/api/search', { observe: 'response', params }).pipe(
  //     map(response => {
  //       const paginatedResult: PaginatedResult<BookDetail[]> = {
  //         items: response.body as BookDetail[],
  //         pagination: JSON.parse(response.headers.get('Pagination')!)
  //       };
  //       return paginatedResult;
  //     })
  //   );
  // }
}
