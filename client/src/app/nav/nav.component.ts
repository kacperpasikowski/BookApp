import { Component, effect, ElementRef, EventEmitter, HostListener, Inject, OnInit, Output } from '@angular/core';
import { BookDetail } from '../models/book-detail.model';
import { BookService } from '../services/book.service';
import { Router } from '@angular/router';
import { SidebarService } from '../services/sidebar.service';
import { User } from '../models/user.model';
import { AccountService } from '../services/account.service';
import { SearchService } from '../services/search.service';
import { SearchResult } from '../models/search-result-model';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent  implements OnInit{
  query: string = '';
  results: SearchResult[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  currentUser: User | null = null;
  @Output() toggleUserPanel = new EventEmitter<void>();


  constructor(private searchService: SearchService, private sidebarService: SidebarService,private router: Router,
    private accountService: AccountService) {
     }


  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => {
      this.currentUser = user;
    })
  }

  

  onSearch(){
    if(this.query.length>1){
      this.searchService.search(this.query, this.pageNumber, this.pageSize).subscribe({
        next : data => {
          if (data && data.items){
            this.results = data.items;
          }else{
            this.results = [];
          }
        },
        error: error => console.log(error)
      });
    }else{
      this.results = [];
    }
  }

  logout(){
    this.accountService.logout();
    this.router.navigate(['/login']);
  }


  goToDetail(type: string, id: string) {
    this.results = [];
    if (type === 'Book') {
      this.router.navigate(['/book', id])
    } else if (type === 'Author') {
      this.router.navigate(['author', id])
    }
  }

  toggleSidebar() {
    this.sidebarService.toggleSidebar();
  }
  
  
}
