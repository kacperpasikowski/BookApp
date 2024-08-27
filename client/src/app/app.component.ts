import { Component, HostListener, inject, OnInit, Output } from '@angular/core';
import { User } from './models/user.model';
import { UserService } from './services/user.service';
import { AccountService } from './services/account.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'client';
  userPanelOpened = false;
  users: User[] = [];
  selectedUser: User | null = null;
  chatWindows: User[] = [];
  private accountService = inject(AccountService);
  currentUser$: Observable<User | null> | undefined ;
  

  constructor(private userService: UserService){
    this.currentUser$ = this.accountService.currentUser$;
    this.currentUser$.subscribe(user => {
      if (!user){
        this.closeAllChats();
      }
    })
  }



  ngOnInit(): void {
    this.loadUsers();
  
  }


  sendMessage(user: any) {
    console.log(`Sending message to ${user.name}`);
    // Implementuj logikę wysyłania wiadomości
  }

  toggleUserPanel() {
    this.userPanelOpened = !this.userPanelOpened;
  }

  loadUsers(){
    this.userService.getAllUsers().subscribe({
      next: users => {
        this.users = users
      },
      error : error => console.log(error)
    })
  }

  openChat(user: User): void{
    const existingChatIndex = this.chatWindows.findIndex(chat => chat.id === user.id);

    if(existingChatIndex === -1){
      if(this.chatWindows.length <4){
        this.chatWindows.push(user);
      }else{
        this.chatWindows.shift();
        this.chatWindows.push(user);
      }
    }
  }

  closeChat(index: number): void {
    this.chatWindows.splice(index, 1);
  }

  closeAllChats(){
    this.chatWindows = [];
  }


  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const clickedInsideDrawer = (event.target as HTMLElement).closest('mat-drawer');
    const clickedInsideToggle = (event.target as HTMLElement).closest('.nav-link');

    if (!clickedInsideDrawer && !clickedInsideToggle) {
      this.userPanelOpened = false;
    }
  }
}
