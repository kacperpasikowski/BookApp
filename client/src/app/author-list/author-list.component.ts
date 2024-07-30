import { Component, inject, OnInit } from '@angular/core';
import { Author } from '../models/author.model';
import { AuthorService } from '../services/author.service';
import { UserParams } from '../models/userParams';
import { AuthorDetail } from '../models/author-detail-model';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {
  authors: AuthorDetail[] = [];
  userParams = new UserParams();
  authorService = inject(AuthorService)

  constructor() { }
  ngOnInit(): void {
    if (!this.authorService.paginatedResult()) this.loadAuthors();
    
  }

  loadAuthors(){
    this.authorService.getAuthors(this.userParams);
  }


  pageChanged(event: any){
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadAuthors();
    }
  }

}
