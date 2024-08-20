import { Component, inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};
  accountService = inject(AccountService);
  router = inject(Router)


  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.router.navigate(['/book']);
      },
      error : error => console.log(error)
    })
  }


}
