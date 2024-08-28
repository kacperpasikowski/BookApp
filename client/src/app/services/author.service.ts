import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from '../models/author.model';
import { AuthorDetail } from '../models/author-detail-model';
import { PaginatedResult } from '../models/pagination';
import { UserParams } from '../models/userParams';
import { MarkAuthorAsFavorite } from '../models/add-fav-author-model';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private apiUrl = "https://localhost:5001/api/author";
  paginatedResult = signal<PaginatedResult<AuthorDetail[]> | null> (null);

  constructor(private http: HttpClient) { }



  getAuthors(userParams: UserParams){
    let params = this.setPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    return this.http.get<AuthorDetail[]>(this.apiUrl, {observe: 'response', params}).subscribe({
      next: response => {
        this.paginatedResult.set({
          items: response.body as AuthorDetail[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    })
  }


  private setPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    if(pageNumber && pageSize){
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize',pageSize);
    }

    return params;
  }

  getAuthor(id: string): Observable<AuthorDetail>{
    return this.http.get<AuthorDetail>(`${this.apiUrl}/${id}`);
  }


  getAllAuthors(){
    return this.http.get<AuthorDetail[]>(this.apiUrl)
  }

  addFavAuthor(model: MarkAuthorAsFavorite, options: any){
    return this.http.post("https://localhost:5001/api/userauthor/",model, options);
  }


  
}
