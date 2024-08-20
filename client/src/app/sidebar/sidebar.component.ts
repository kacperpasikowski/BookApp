import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';
import { AccountService } from '../services/account.service';

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
  accountService = inject(AccountService);
  currentUser: User | null = null;



  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.currentUser = this.accountService.currentUser();
    this.loadUsers()
  }

  selectUser(user: User): void {
    this.userSelected.emit(user);
    console.log("user selected:", user);
  }


  loadUsers(){
    this.userService.getAllUsers().subscribe({
      next: users =>{
        this.users = users.filter(user => user.userName !== this.currentUser?.userName);
      },
      error: error => console.log(error)
    });
  }

  



  
}
