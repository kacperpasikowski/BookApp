import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private sidebarOpenedSubject = new BehaviorSubject<boolean>(false);
  sidebarOpened$ = this.sidebarOpenedSubject.asObservable();

  toggleSidebar() {
    this.sidebarOpenedSubject.next(!this.sidebarOpenedSubject.value);
  }

  openSidebar() {
    this.sidebarOpenedSubject.next(true);
  }

  closeSidebar() {
    this.sidebarOpenedSubject.next(false);
  }
}
