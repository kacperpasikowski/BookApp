import { Component, Input } from '@angular/core';
import { BookSummary } from '../models/book-summary.model';
import { AuthorService } from '../services/author.service';

@Component({
  selector: 'app-carousel-book',
  templateUrl: './carousel-book.component.html',
  styleUrls: ['./carousel-book.component.css']
})
export class CarouselBookComponent {
  @Input() books: BookSummary[] = [];
  
  itemsPerSlide = 3;
  singleSlideOffset = false;
  noWrap = false;
  slidesChangeMessage = '';

  ngOnInit(): void {}

  onSlideRangeChange(indexes: number[] | void): void {
    this.slidesChangeMessage = `Slides have been switched: ${indexes}`;
  }
}