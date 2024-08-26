import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Message } from '../models/messsage-model';

@Injectable({
  providedIn: 'root'
})
export class ChatStateService {
  private messageSubject = new BehaviorSubject<Message[]>([]);
  private currentPageSubject = new BehaviorSubject<number>(1);
  private allMessagesLoadedSubject = new BehaviorSubject<boolean>(false);

  messages$ = this.messageSubject.asObservable()
  currentPage$ = this.currentPageSubject.asObservable()
  allMessagesLoaded$ = this.allMessagesLoadedSubject.asObservable()

  

  setMessages(messages: Message[]) {
    this.messageSubject.next(messages);
  }

  getMessages(): Message[]{
    return this.messageSubject.value;
  }

  setCurrentPage(page: number){
    this.currentPageSubject.next(page);
  }

  getCurrentPage() : number{
    return this.currentPageSubject.value;
  }

  setAllMessagesLoaded(loaded: boolean) {
    this.allMessagesLoadedSubject.next(loaded);
  }

  getAllMessagesLoaded(): boolean{
    return this.allMessagesLoadedSubject.value;
  }

  clearState() {
    this.messageSubject.next([]);
    this.currentPageSubject.next(1);
    this.allMessagesLoadedSubject.next(false);
  }
}
