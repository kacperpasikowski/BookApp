import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Publisher } from '../models/publisher.model';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {
  private apiUrl = "https://localhost:5001/api/publisher"
  constructor(private http: HttpClient) { }



  getPublishers():Observable<Publisher[]>{
    return this.http.get<Publisher[]>(this.apiUrl);
  }
}
