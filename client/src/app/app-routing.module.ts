import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './book-list/book-list.component';
import { BookCardComponent } from './book-card/book-card.component';
import { BookdetailComponent } from './bookdetail/bookdetail.component';
import { AuthorListComponent } from './author-list/author-list.component';
import { AuthorDetailComponent } from './author-detail/author-detail.component';
import { AddBookComponent } from './Admin/add-book/add-book.component';
import { AdminBookListComponent } from './Admin/admin-book-list/admin-book-list.component';
import { LoginComponent } from './login/login.component';
import { UserProfileComponent } from './user-rofile/user-profile.component';

const routes: Routes = [
  {path: 'book/:id', component: BookdetailComponent},
  {path: 'author', component: AuthorListComponent},
  {path: 'author/:id', component: AuthorDetailComponent},
  {path: 'admin/add', component: AddBookComponent},
  {path: 'admin/list', component: AdminBookListComponent},
  {path: 'login', component: LoginComponent},
  {path: 'user/:userName', component: UserProfileComponent},
  {path: '**', component: BookListComponent, pathMatch: 'full'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
