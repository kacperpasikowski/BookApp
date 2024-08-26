import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AdminBookListComponent } from './Admin/admin-book-list/admin-book-list.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SidebarComponent } from './sidebar/sidebar.component';
import { MatDividerModule } from '@angular/material/divider';
import { ChatWindowComponent } from './chat-window/chat-window.component';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { UserProfileComponent, } from './user-rofile/user-profile.component'



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
    CarouselBookComponent,
    AdminBookListComponent,
    SidebarComponent,
    ChatWindowComponent,
    LoginComponent,
    UserProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    CarouselModule.forRoot(),
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    MatSidenavModule,
    MatProgressSpinnerModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatDividerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
