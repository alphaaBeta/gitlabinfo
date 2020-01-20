import { Component, Inject, OnInit, Input } from '@angular/core';
import { StorageService } from '../service/storage/storage.service';
import { IUserDetailed } from '../account/user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  @Input() user: IUserDetailed;
  isExpanded = false;
  apiLoginUrl: string;
  apiLogoutUrl: string;

  ownerModeEnabled: boolean;
  // userLoggedIn: boolean;

  constructor(@Inject('BASE_URL') private baseUrl: string, private storage: StorageService) {
    this.apiLoginUrl = baseUrl + 'api/account/login';
    this.apiLogoutUrl = baseUrl + 'api/account/logout';
  }

  ngOnInit(): void {
    this.ownerModeEnabled = this.storage.isOwnerModeEnabled();
    // this.userLoggedIn = this.storage.isAccountLoggedIn();
  }

  // login() {
  //   this.userLoggedIn = true;
  // }

  // logout() {
  //   this.userLoggedIn = false;
  // }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  switchOwnerMode() {
    if (this.ownerModeEnabled) {
      this.storage.setOwnerMode(false);
      this.ownerModeEnabled = false;
    } else {
      this.storage.setOwnerMode(true);
      this.ownerModeEnabled = true;
    }
    window.location.reload();
  }
}
