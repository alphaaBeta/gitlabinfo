import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from './user';
import { AccountService } from '../service/account/account.service';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit {
  user: IUser;
  errorMessage: string;

  constructor(private accountService: AccountService) {

  }

  getAccount(): void {
    this.accountService.getAccount().subscribe(
      account => {
        this.user = account;
      },
      error => this.errorMessage = <any>error
    );
  }

  ngOnInit(): void {
    this.getAccount();
  }
}
