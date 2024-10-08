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
    this.accountService.currentUser$.subscribe(user=>{
      this.currentUser = user;
      if(user){
        this.loadUsers();
      }
      
    })
    
  }

  selectUser(user: User): void {
    this.userSelected.emit(user);
  }


  loadUsers(){
    this.userService.getFriends().subscribe({
      next: users =>{
        this.users = users.filter(user => user.userName.toLowerCase() !== this.currentUser?.userName.toLowerCase());
      },
    error: error => console.log(error)
    });
  }
}
