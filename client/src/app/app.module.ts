import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BookListComponent } from './book-list/book-list.component';
import { BookdetailComponent } from './bookdetail/bookdetail.component';
import { BookCardComponent } from './book-card/book-card.component';
import { AuthorListComponent } from './author-list/author-list.component';
import { AuthorCardComponent } from './author-card/author-card.component';
import { AuthorDetailComponent } from './author-detail/author-detail.component';
import { AddBookComponent } from './Admin/add-book/add-book.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarouselBookComponent } from './carousel-book/carousel-book.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    BookListComponent,
    BookdetailComponent,
    BookCardComponent,
    AuthorListComponent,
    AuthorCardComponent,
    AuthorDetailComponent,
    AddBookComponent,
    CarouselBookComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    CarouselModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
