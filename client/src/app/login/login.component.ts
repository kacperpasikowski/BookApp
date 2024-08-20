import { Component, inject } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};
  accountService = inject(AccountService);


  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error : error => console.log(error)
    })
  }

  logout(){
    this.accountService.logout();
  }

}
