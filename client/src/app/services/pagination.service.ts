import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationService {
  private pageNumber =1;
  private pageSize = 10;

  constructor() { }

  setPageNumber(pageNumber: number){
    this.pageNumber = pageNumber;
  }

  getPageNumber(): number{
    return this.pageNumber;
  }

  setPageSize(pageSize: number){
    this.pageSize = pageSize;
  }

  getPageSize(): number{
    return this.pageSize;
  }

  getPaginationParams(): HttpParams{
    let params = new HttpParams();
    params = params.append('pageNumber', this.pageNumber.toString());
    params = params.append('pageSize', this.pageSize.toString());
    return params;
  }

  resetPagination(){
    this.pageNumber = 1;
  }
}
