import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserDetailed } from './user';
import { AccountService } from '../service/account/account.service';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit {
  user: IUserDetailed;
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
