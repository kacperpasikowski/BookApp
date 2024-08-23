import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { PaginatedResult } from '../models/pagination';
import { Message } from '../models/messsage-model';
import { PaginationService } from './pagination.service';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = "https://localhost:5001/api/message/"
  private http = inject(HttpClient);
  private paginationService = inject(PaginationService);
  paginatedResult = signal<PaginatedResult<Message[]> | null>(null);

  constructor() { }



  getMessageThread(username: string): Observable<PaginatedResult<Message[]>>{
    const params = this.paginationService.getPaginationParams();
    return this.http.get<Message[]>(this.baseUrl + 'thread/'+ username,  {observe: 'response', params }).pipe(
      map(response => {
        const paginatedResult: PaginatedResult<Message[]> = {
          items: response.body || [],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        };
        return paginatedResult;
      })
    );
  }


  sendMessage(username: string, content: string){
    return this.http.post<Message>(this.baseUrl, {recipientUsername: username, content})
  }


}
