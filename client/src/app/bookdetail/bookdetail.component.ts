import { Component, inject, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { ActivatedRoute } from '@angular/router';
import { BookDetail } from '../models/book-detail.model';

@Component({
  selector: 'app-bookdetail',
  templateUrl: './bookdetail.component.html',
  styleUrls: ['./bookdetail.component.css']
})
export class BookdetailComponent implements OnInit {
  
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  book: BookDetail = {} as BookDetail;



  ngOnInit(): void {
    this.loadBook();
  }

  loadBook(){
    const id = this.route.snapshot.paramMap.get('id');
    if(!id) return;
    this.bookService.getBook(id).subscribe({
      next: book => {
        this.book = book;
        console.log(this.book);
      }
    })
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
