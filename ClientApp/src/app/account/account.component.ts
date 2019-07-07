import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from './user';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit{
  public user: IUser;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IUser>(baseUrl + 'api/account').subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }
  ngOnInit(): void {

  }
}
