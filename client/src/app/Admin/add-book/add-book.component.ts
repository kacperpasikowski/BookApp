import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthorService } from 'src/app/services/author.service';
import { CategoryService } from 'src/app/services/category.service';
import { AdminBookService } from '../AdminServices/admin-book.service';
import { Category } from 'src/app/models/category.model';
import { Author } from 'src/app/models/author.model';
import { PublisherService } from 'src/app/services/publisher.service';
import { Publisher } from 'src/app/models/publisher.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);
  private authorService = inject(AuthorService);
  private publisherService = inject(PublisherService);
  private adminBookService = inject(AdminBookService);
  private router = inject(Router);


  bookForm: FormGroup = new FormGroup({});
  categories: Category[] = [];
  authors: Author[] = [];
  publishers: Publisher[] = [];


  ngOnInit(): void {
    this.loadForm();
    this.loadPublishers();
    this.loadAuthors();
    this.loadCategories();


  }


  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: categories => this.categories = categories,
      error: error => error = console.log(error)
    });
  }

  loadAuthors() {
    this.authorService.getAuthors().subscribe({
      next: authors => this.authors = authors,
      error: error => error = console.log(error)
    });
  }

  loadPublishers() {
    this.publisherService.getPublishers().subscribe({
      next: publishers => this.publishers = publishers,
      error: error => error = console.log(error)
    })
  }

  loadForm() {
    this.bookForm = this.fb.group({
      
      title: ['', Validators.required],
      description: ['', Validators.required],
      dateOfPublish: ['', Validators.required],
      publisherId: ['', Validators.required],
      bookAvatarUrl: ['', Validators.required],
      authorIds: [[], Validators.required],
      categoryIds: [[], Validators.required],
    })

  }

  onSubmit() {
    if (this.bookForm?.valid) {
      this.adminBookService.addBook(this.bookForm.value).subscribe({
        next: response => { 
          console.log(response);
          this.router.navigate([''])
        },
        error: error => console.log(error)
      });
    }
  }



}
