import { Component, inject, OnInit } from '@angular/core';
import { AuthorService } from '../services/author.service';
import { ActivatedRoute } from '@angular/router';
import { AuthorDetail } from '../models/author-detail-model';
import { BookSummary } from '../models/book-summary.model';
import { AccountService } from '../services/account.service';
import { User } from '../models/user.model';
import { MarkAuthorAsFavorite } from '../models/add-fav-author-model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-author-detail',
  templateUrl: './author-detail.component.html',
  styleUrls: ['./author-detail.component.css']
})
export class AuthorDetailComponent implements OnInit {
  private authorService = inject(AuthorService);
  private route = inject(ActivatedRoute);
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  author: AuthorDetail = {} as AuthorDetail;
  books: BookSummary[] = [];
  currentUser: User | null = null;
  

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe( user => {
      this.currentUser = user;
    })
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

  addAuthorToFavorites() : void {
    if (this.currentUser){
      const model: MarkAuthorAsFavorite = {authorId: this.author.id};
      this.authorService.addFavAuthor(model, {headers: {skipErrorHandling: '400'}}).subscribe({
        next: () => {
          this.toastr.success(`You have added ${this.author.name} as your favorite author` );
          this.loadAuthor();
        },
        error: error => this.toastr.error("This author is already in your favorite authors collection")
    })
  }
  }
}
