import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent {
  public user: IUser;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IUser>(baseUrl + 'api/account').subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }
}


interface IUser {
  id: number;
  name: string;
  email: string;
  login: string;
  webUrl: string;
  avatarUrl: string;
}
