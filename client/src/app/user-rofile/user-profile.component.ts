import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { BookDetail } from '../models/book-detail.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-rofile',
  templateUrl: './user-Profile.component.html',
  styleUrls: ['./user-Profile.component.css']
})
export class UserProfileComponent implements OnInit{
  userService = inject(UserService);
  private route = inject(ActivatedRoute);
  user : User | null = null;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const userName = params.get('userName');
      if(userName){
        this.loadUser(userName);
      }
    })
    
  }

  loadUser(userName: string){
    this.userService.getUserByUsername(userName).subscribe({
      next: user => {
        this.user = user;
      },
      error: error => console.log(error)
    })

  }


  
  


}
