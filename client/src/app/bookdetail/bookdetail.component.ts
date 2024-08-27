import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { ActivatedRoute } from '@angular/router';
import { BookDetail } from '../models/book-detail.model';
import { Subscription } from 'rxjs';
import { GradeModel } from '../models/grade-model';
import { User } from '../models/user.model';
import { AccountService } from '../services/account.service';
import { MarkBookAsRead } from '../models/add-read-book-model';

@Component({
  selector: 'app-bookdetail',
  templateUrl: './bookdetail.component.html',
  styleUrls: ['./bookdetail.component.css']
})
export class BookdetailComponent implements OnInit, OnDestroy {
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  private accountService = inject(AccountService);
  book: BookDetail = {} as BookDetail;
  routeSubscription: Subscription | undefined;
  selectedGrade = 0;
  currentUser: User | null = null;

  hoveredGrade: number = 0;



  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user =>{
      this.currentUser = user;
    })
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
        this.selectedGrade = 0;
        this.hoveredGrade = 0;
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

  getStarArray() : number[] {
    return Array.from({length: 5}, (_, index) => index+1);
  }

  selectGrade(star: number){
    this.selectedGrade = star;
    this.hoveredGrade = 0;
  }

  hoverGrade(star: number){
    this.hoveredGrade = star;
  }
  resetHover(){
    this.hoveredGrade = 0;
  }

  submitGrade() : void{
    if(this.selectedGrade > 0){
      const gradeModel: GradeModel = {bookId: this.book.id, grade: this.selectedGrade};
      this.bookService.addOrUpdateGrade(gradeModel).subscribe({
        next: () => {
          console.log("updadet succesfully");
          this.loadBook(this.book.id);
        },
        error: error => console.log(error)
      })
    }
  }

  markBookAsRead(): void {
    if(this.currentUser){
      const model: MarkBookAsRead = { bookId: this.book.id, dateRead: new Date().toISOString().split('T')[0]};
      this.bookService.addReadBook(model).subscribe({
        next: () => {
          console.log("you have added book to your profile!");
          this.loadBook(this.book.id);
        },
        error: error => console.log(error)
      })
    }
  }


}
