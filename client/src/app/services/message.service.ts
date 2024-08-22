import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { PaginatedResult } from '../models/pagination';
import { Message } from '../models/messsage-model';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = "https://localhost:5001/api/message/"
  private http = inject(HttpClient);
  paginatedResult = signal<PaginatedResult<Message[]> | null>(null);

  constructor() { }



  getMessageThread(username: string){
    return this.http.get<Message[]>(this.baseUrl + 'thread/'+ username);
  }


}
