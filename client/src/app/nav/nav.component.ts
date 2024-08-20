import { Component, ElementRef, EventEmitter, HostListener, Inject, Output } from '@angular/core';
import { BookDetail } from '../models/book-detail.model';
import { BookService } from '../services/book.service';
import { Router } from '@angular/router';
import { SidebarService } from '../services/sidebar.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  query: string = '';
  results: BookDetail[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  @Output() toggleUserPanel = new EventEmitter<void>();


  constructor(private bookService: BookService, private sidebarService: SidebarService,private router: Router,
     private elementRef: ElementRef) { }

  

  onSearch() {
    if (this.query.length > 1) {
      this.bookService.searchBooks(this.query, this.pageNumber, this.pageSize).subscribe({
        next: data => {
          if (data && data.items) {
            this.results = data.items;
            
          } else {
            this.results = [];
          }
        },
        error: error => console.log('Search error:', error) 
      });
    } else {
      this.results = [];
      
    }
  }


  goToDetail(type: string, id: string) {
    this.results = [];
    if (type === 'book') {
      this.router.navigate(['/book', id])
    } else if (type === 'author') {
      this.router.navigate(['author', id])
    }
  }

  toggleSidebar() {
    this.sidebarService.toggleSidebar();
  }
  
}
