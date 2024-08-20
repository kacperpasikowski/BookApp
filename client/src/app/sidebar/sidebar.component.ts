import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  @Input() users: User[] = [];
  @Input() userPanelOpened = false;
  @Output() toggleUserPanel = new EventEmitter<void>();
  @Output() userSelected = new EventEmitter<User>();



  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.loadUsers()
  }

  selectUser(user: User): void {
    this.userSelected.emit(user);
    console.log("user selected:", user);
  }


  loadUsers(){
    this.userService.getAllUsers().subscribe({
      next: users =>{
        this.users = users;
      },
      error: error => console.log(error)
    });
  }

  



  
}
