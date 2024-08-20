import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = "https://localhost:5001/api/user"
  constructor(private http: HttpClient) { }



  getAllUsers(): Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl);
  }
}