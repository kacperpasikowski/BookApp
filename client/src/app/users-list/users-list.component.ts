import { Component, inject, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { UserParams } from '../models/userParams';
import { PaginationService } from '../services/pagination.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users: User[] = [];
  userService = inject(UserService);
  userParams = new UserParams();

  constructor(){}

  ngOnInit(): void {
    if(!this.userService.paginatedResult())this.loadUsers();
  }




  loadUsers() {
    this.userService.getAllUsers(this.userParams);
    
  }


  pageChanged(event: any) {
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadUsers();
    }
  }
}


