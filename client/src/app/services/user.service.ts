import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { BookDetail } from '../models/book-detail.model';
import { PaginatedResult } from '../models/pagination';
import { PaginationService } from './pagination.service';
import { UserParams } from '../models/userParams';
import { AddFriendRequestModel } from '../models/add-friend-request.model';
import { ToastrService } from 'ngx-toastr';
import { FriendRequest } from '../models/friend-request-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = "https://localhost:5001/api/"
  paginatedResult = signal<PaginatedResult<User[]> | null>(null);
  toastr = inject(ToastrService);
  
  
  constructor(private http: HttpClient) { }



  getAllUsers(userParams: UserParams) {
    let params = this.setPaginationHeader(userParams.pageNumber, userParams.pageSize);
    this.http.get<User[]>(this.apiUrl+ 'user', {observe: 'response', params}).subscribe({
      next: response => {
        this.paginatedResult.set({
          items: response.body as User[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    })
  }
  
  setPaginationHeader(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    if(pageNumber && pageSize){
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    return params;
  }
  
  getUserByUsername(userName: string): Observable<User>{
    return this.http.get<User>(this.apiUrl + 'user/' + userName)
  }

  getUserBooks(userId: string): Observable<BookDetail[]> {
    return this.http.get<BookDetail[]>(`${this.apiUrl}userbook/${userId}`)
  }

  sendFriendRequest(model: AddFriendRequestModel ){
    return this.http.post(this.apiUrl + 'friends/send', model).subscribe({
      next: (response) => {
        this.toastr.success('You sent a friend request');
        console.log(response);
      },
      error: error => console.log(error)
    })
  }

  getPendingFriendRequest() : Observable<FriendRequest[]>{
    return this.http.get<FriendRequest[]>(this.apiUrl + 'friends/pending');
  }

  acceptFriendRequest(requestId: string){
    return this.http.post(this.apiUrl + 'friends/accept/' + requestId, {})
  }

  rejectFriendRequest(requestId: string){
    return this.http.post(this.apiUrl + 'friends/reject/' + requestId, {})
  }

  getFriends(): Observable<User[]>{
    return this.http.get<User[]>(this.apiUrl + 'friends');
  }

  getFavoriteCategories(userId: string): Observable<string[]>{
    return this.http.get<string[]>(`${this.apiUrl}user/${userId}/favorite-categories`)
  }


  
  
}
