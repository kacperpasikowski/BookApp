import { Component, ElementRef, HostListener } from '@angular/core';
import { BookDetail } from '../models/book-detail.model';
import { BookService } from '../services/book.service';
import { Router } from '@angular/router';

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

  constructor(private bookService: BookService, private router: Router, private elementRef: ElementRef) { }


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

  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event){
    if(!this.elementRef.nativeElement.contains(event.target)){
      this.results = [];
    }
  }
}
