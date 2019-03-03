import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent {
  public user: GitLabUser;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<GitLabUser>(baseUrl + 'api/account').subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }
}


interface GitLabUser {
  gitLabId: number;
  gitLabName: string;
  gitLabEmail: string;
  gitLabLogin: string;
  gitLabWebUrl: string;
  gitLabAvatarUrl: string;
}
