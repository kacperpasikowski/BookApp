import { Component, inject, OnInit } from '@angular/core';
import { AuthorService } from '../services/author.service';
import { ActivatedRoute } from '@angular/router';
import { AuthorDetail } from '../models/author-detail-model';
import { BookSummary } from '../models/book-summary.model';

@Component({
  selector: 'app-author-detail',
  templateUrl: './author-detail.component.html',
  styleUrls: ['./author-detail.component.css']
})
export class AuthorDetailComponent implements OnInit {
  private authorService = inject(AuthorService);
  private route = inject(ActivatedRoute);
  author: AuthorDetail = {} as AuthorDetail;
  books: BookSummary[] = [];
  

  ngOnInit(): void {
    this.loadAuthor();
    console.log()
  }


  loadAuthor(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      console.error('No author ID found in route parameters.');
      return;
    }

    this.authorService.getAuthor(id).subscribe({
      next: author => {
        console.log('Author details:', author);
        this.author = author;
        this.books = author.books;
      },
      error: error => console.error('Error loading author details:', error)
    });
  }

  getAuthorBooks(): { id: string, title: string }[] {
    if (!this.author.books || this.author.books.length === 0) {
      return [];
    }
    return this.author.books.map(book => ({ id: book.id, title: book.title }));
  }



}
