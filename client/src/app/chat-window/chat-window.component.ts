import { AfterViewInit, Component, ElementRef, EventEmitter, inject, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { User } from '../models/user.model';
import { Message } from '../models/messsage-model';
import { MessageService } from '../services/message.service';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.css']
})
export class ChatWindowComponent implements OnInit, AfterViewInit{
  

  @Input() selectedUser!: User;
  @Output() closeChat = new EventEmitter<void>();
  @ViewChild('chatContent') chatContent!: ElementRef;
  private messageService = inject(MessageService);
  private accountService = inject(AccountService);
  messages: Message[] = [];
  messageContent: string = '';
  currentUser: User | null =null;

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user =>{
      this.currentUser = user;
    })
    this.loadMessageThread();
    
  }

  loadMessageThread(): void{
    if(this.selectedUser){
      this.messageService.getMessageThread(this.selectedUser.userName).subscribe({
        next: messages => {
          this.messages = messages;
          this.scrollToBottom();
        },
        error: error => console.log(error)
      })
    }
  }

  ngAfterViewInit(): void {
    this.scrollToBottom();
  }

  sendMessage(){

  }

  scrollToBottom(): void {
    setTimeout(() => {
      if (this.chatContent) {
        this.chatContent.nativeElement.scrollTop = this.chatContent.nativeElement.scrollHeight;
      }
    }, 100);  
  }



  

}
