import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from '../models/author.model';
import { AuthorDetail } from '../models/author-detail-model';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private apiUrl = "https://localhost:5001/api/author";

  constructor(private http: HttpClient) { }



  getAuthors(): Observable<Author[]>{
    return this.http.get<Author[]>(this.apiUrl);
  }

  getAuthor(id: string): Observable<AuthorDetail>{
    return this.http.get<AuthorDetail>(`${this.apiUrl}/${id}`);
  }
  
}
