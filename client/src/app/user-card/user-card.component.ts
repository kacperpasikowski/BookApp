import { Component, inject, Input, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { AddFriendRequestModel } from '../models/add-friend-request.model';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  userService = inject(UserService);
  favoriteCategories: string[]=[];
  @Input() user!: User;


  ngOnInit(): void {
    this.loadFavCategories();
  }

  onSendFriendRequest(){
    const model: AddFriendRequestModel = {toUserId: this.user.id};
    this.userService.sendFriendRequest(model);

  }

  loadFavCategories(){
    this.userService.getFavoriteCategories(this.user.id).subscribe({
      next: categories => {
        this.favoriteCategories = categories;
      },
      error: error => console.log(error)
    })
  }


  
}
