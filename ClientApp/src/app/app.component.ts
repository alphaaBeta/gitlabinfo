import { Component, OnInit } from '@angular/core';
import { IUserDetailed } from './account/user';
import { AccountService } from './service/account/account.service';
import { StorageService } from './service/storage/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  user: IUserDetailed;

  constructor(private accountService: AccountService, private storage: StorageService) {
  }
  ngOnInit(): void {
    this.accountService.getAccount().subscribe(acc => {
      this.user = acc;
      if (acc) {
        this.userLoggedIn();
      } else {
        this.userNotLoggedIn();
      }
    }, error => {
      console.error(error);
      this.userNotLoggedIn();
    });
  }

  private userLoggedIn() {
    this.storage.setAccountLoggedIn(true);
  }
  private userNotLoggedIn() {
    this.storage.setAccountLoggedIn(false);
  }
}
