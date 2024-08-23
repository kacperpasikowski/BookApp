import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ElementRef, EventEmitter, inject, Inject, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { User } from '../models/user.model';
import { Message } from '../models/messsage-model';
import { MessageService } from '../services/message.service';
import { AccountService } from '../services/account.service';
import { PaginationService } from '../services/pagination.service';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.css']
})
export class ChatWindowComponent implements OnInit, AfterViewInit{
  
  

  @Input() selectedUser!: User;
  @Output() closeChat = new EventEmitter<void>();
  @ViewChild('chatContent', {static: false}) chatContent!: ElementRef;
  private cdr = inject(ChangeDetectorRef);
  private messageService = inject(MessageService);
  private accountService = inject(AccountService);
  private paginationService = inject(PaginationService);
  messages: Message[] = [];
  messageContent: string = '';
  currentUser: User | null =null;
  loading = false;
  autoScrollEnabled = true;
  allMessagesLoaded = false;
  showLoader = false;


  

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user =>{
      this.currentUser = user;
    })
    this.loadMessageThread();
  }
  
  ngAfterViewInit(): void {
    this.scrollToBottom();
  }

 

  loadMessageThread(): void {
    if (this.selectedUser && !this.allMessagesLoaded) {
      this.loading = true;
      
      this.messageService.getMessageThread(this.selectedUser.userName).subscribe({
        next: result => {
          if(result.items)
          {if (result.items.length < this.paginationService.getPageSize()) {
            this.allMessagesLoaded = true;
          }}
          this.messages = [...result.items!.reverse(), ...this.messages];
          this.loading = false;
          this.showLoader = false;
          
          this.cdr.detectChanges();
          if(this.autoScrollEnabled){
            this.scrollToBottom();
            this.autoScrollEnabled = false;
          }
        },
        error: error => {
          console.log(error);
          this.loading = false;
        }
      });
    }
  }


  loadMoreMessages(): void {
    if (!this.allMessagesLoaded && !this.loading) {
      this.showLoader = true;  
      setTimeout(() => {
        this.paginationService.setPageNumber(this.paginationService.getPageNumber() + 1);
        this.loadMessageThread();
      }, 1000);  
    }
  }

  onScrollTop(event: any): void {
    const element = event.target;
    if (element.scrollTop == 0 && !this.loading) {
      this.loadMoreMessages(); 
    }
  }


  scrollToBottom(): void {
    if (this.chatContent) {
      try {
        this.chatContent.nativeElement.scrollTop = this.chatContent.nativeElement.scrollHeight;
      } catch (err) {
        console.error("Scroll error:", err);
      }
    }
  }

  sendMessage(){
    this.messageService.sendMessage(this.selectedUser.userName, this.messageContent).subscribe({
      next: message => {
        this.messages.push(message);
        this.messageContent = '';
        
        this.cdr.detectChanges();
        this.scrollToBottom();

      },
      error: error => console.log(error)
    })
  }

  
}
