import { Component, effect, ElementRef, EventEmitter, HostListener, inject, Inject, OnInit, Output } from '@angular/core';
import { BookDetail } from '../models/book-detail.model';
import { BookService } from '../services/book.service';
import { Router } from '@angular/router';
import { SidebarService } from '../services/sidebar.service';
import { User } from '../models/user.model';
import { AccountService } from '../services/account.service';
import { SearchService } from '../services/search.service';
import { SearchResult } from '../models/search-result-model';
import { UserService } from '../services/user.service';
import { FriendRequest } from '../models/friend-request-model';

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
  userService = inject(UserService);
  pendingRequests: FriendRequest[] = [];
  @Output() toggleUserPanel = new EventEmitter<void>();


  constructor(private searchService: SearchService, private sidebarService: SidebarService,private router: Router,
    private accountService: AccountService) {
     }


  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => {
      this.currentUser = user;
      this.loadPendingRequests();
    })
    
  }



  loadPendingRequests() {
    this.userService.getPendingFriendRequest().subscribe({
      next: requests => {
        this.pendingRequests = requests
      },
      error: error => console.log(error)
    })
  }

  acceptFriendRequest(requestId: string){
    this.userService.acceptFriendRequest(requestId).subscribe({
      next: () => {
        this.loadPendingRequests();
        this.ngOnInit();
      },
      error: error => console.log(error)
    })
  }

  rejectFriendRequest(requestId: string){
    this.userService.rejectFriendRequest(requestId).subscribe({
      next: () => {
        this.ngOnInit();
      },
      error: error => console.log(error)
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
