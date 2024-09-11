import { ChangeDetectorRef, Component, HostListener, inject, OnInit, Output } from '@angular/core';
import { User } from './models/user.model';
import { UserService } from './services/user.service';
import { AccountService } from './services/account.service';
import { Observable } from 'rxjs';
import { SidebarService } from './services/sidebar.service';

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
  sidebarService = inject(SidebarService);
  currentUser$: Observable<User | null> | undefined ;
  cdr = inject(ChangeDetectorRef)
  

  constructor(private userService: UserService){
    this.currentUser$ = this.accountService.currentUser$;
    this.currentUser$.subscribe(user => {
      if (!user){
        this.closeAllChats();
      }
    })
  }



  ngOnInit(): void {
  }



  toggleUserPanel() {
    this.userPanelOpened = !this.userPanelOpened;
  }

  

  openChat(user: User): void {

  const existingChat = this.chatWindows.find(chatWindow => chatWindow.userName === user.userName);

  if (!existingChat) { 
    if (this.chatWindows.length < 4) {
      this.chatWindows.push(user);
    } else {
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
