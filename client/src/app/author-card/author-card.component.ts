import { Component, Input } from '@angular/core';
import { Author } from '../models/author.model';

@Component({
  selector: 'app-author-card',
  templateUrl: './author-card.component.html',
  styleUrls: ['./author-card.component.css']
})
export class AuthorCardComponent {
  @Input() author!: Author;
  

}
