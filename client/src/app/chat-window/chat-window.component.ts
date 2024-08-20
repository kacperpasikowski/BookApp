import { Component, EventEmitter, Input, Output } from '@angular/core';
import { User } from '../models/user.model';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.css']
})
export class ChatWindowComponent {
  @Input() selectedUser!: User;
  @Output() closeChat = new EventEmitter<void>();

  message: string = '';


  sendMessage(){
    console.log(`Sending message to ${this.selectedUser.userName}: ${this.message}`)
    this.message = '';
  }

  

}
