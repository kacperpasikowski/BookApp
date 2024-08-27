import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { BookDetail } from '../models/book-detail.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = "https://localhost:5001/api/"
  constructor(private http: HttpClient) { }



  getAllUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl + 'user');
  }
  
  getUserByUsername(userName: string): Observable<User>{
    return this.http.get<User>(this.apiUrl + 'user/' + userName)
  }

  getUserBooks(userId: string): Observable<BookDetail[]> {
    return this.http.get<BookDetail[]>(`${this.apiUrl}userbook/${userId}`)
  }

  
  
}
