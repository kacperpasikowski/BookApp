import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination';
import { SearchResult } from '../models/search-result-model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  baseUrl = 'https://localhost:5001/api/search';

  constructor(private http: HttpClient) { }
  


  search(query: string, pageNumber: number =1, pageSize:number = 10):Observable<PaginatedResult<SearchResult[]>>{
    let params = new HttpParams();
    params = params.append('query', query)
    params = params.append('pageNumber', pageNumber.toString())
    params = params.append('query', pageSize.toString())

    return this.http.get<SearchResult[]>(this.baseUrl, {observe: 'response', params}).pipe(
      map(response => {
        const paginatedResult: PaginatedResult<SearchResult[]> = {
          items: response.body as SearchResult[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        };
        return paginatedResult;
      })
    )

  }

}
