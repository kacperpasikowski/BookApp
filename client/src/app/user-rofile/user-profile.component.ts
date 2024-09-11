import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { BookDetail } from '../models/book-detail.model';
import { ActivatedRoute } from '@angular/router';
import { FriendRequest } from '../models/friend-request-model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-Profile.component.html',
  styleUrls: ['./user-Profile.component.css']
})
export class UserProfileComponent implements OnInit{
  userService = inject(UserService);
  private route = inject(ActivatedRoute);
  pendingRequests: FriendRequest[] =[];
  user : User | null = null;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const userName = params.get('userName');
      if(userName){
        this.loadUser(userName);
      }
    })
    this.loadPendingRequests();
    
  }

  loadUser(userName: string){
    this.userService.getUserByUsername(userName).subscribe({
      next: user => {
        this.user = user;
      },
      error: error => console.log(error)
    })
  }

  loadPendingRequests() {
    this.userService.getPendingFriendRequest().subscribe({
      next: requests => {
        this.pendingRequests = requests
      },
      error: error => console.log(error)
    })
  }

  acceptFriendRequest(requestId: string){
    this.userService.acceptFriendRequest(requestId).subscribe({
      next: () => {
        this.loadPendingRequests();
        this.ngOnInit();
      },
      error: error => console.log(error)
    })
  }

  rejectFriendRequest(requestId: string){
    this.userService.rejectFriendRequest(requestId).subscribe({
      next: () => {
        this.ngOnInit();
      },
      error: error => console.log(error)
    })
  }



}
