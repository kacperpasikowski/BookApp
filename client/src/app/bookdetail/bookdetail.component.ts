import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { ActivatedRoute } from '@angular/router';
import { BookDetail } from '../models/book-detail.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-bookdetail',
  templateUrl: './bookdetail.component.html',
  styleUrls: ['./bookdetail.component.css']
})
export class BookdetailComponent implements OnInit, OnDestroy {
  
  
  
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  book: BookDetail = {} as BookDetail;
  routeSubscription: Subscription | undefined;



  ngOnInit(): void {
    this.routeSubscription = this.route.params.subscribe(params => {
      const id = params['id'];
      this.loadBook(id);
    });
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  loadBook(id: string) {
    if (!id) return;
    this.bookService.getBook(id).subscribe({
      next: book => {
        this.book = book;
        console.log(this.book);
      }
    });
  }


  getAuthors(): {id: string, name: string}[]{
    if(!this.book.authors || this.book.authors.length===0){
      return [];
    }
    return this.book.authors.map(author => ({id: author.id, name: author.name}));
  }
  getCategories(): string {
    if(!this.book.categories || this.book.categories.length===0){
      return 'Unknown'
    }
    return this.book.categories.map(category => category.name).join(', ') || "Unknown"
  }
}
