import { Component, Input } from '@angular/core';
import { BookSummary } from '../models/book-summary.model';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.css']
})
export class BookCardComponent {
  @Input() book!: BookSummary;
  constructor(){}

}
