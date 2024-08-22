import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../models/user.model';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  private currentUserSource = new BehaviorSubject<User | null> (null);
  
  currentUser$ = this.currentUserSource.asObservable();

  constructor(){
    this.setCurrentUser();
  }
  

  login(model: any){
    return this.http.post<User>(this.baseUrl + 'auth/login', model).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString);
    this.currentUserSource.next(user);
  }

  getCurrentUser(): User | null {
    return this.currentUserSource.value;
  }
}
