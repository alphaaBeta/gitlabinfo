import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  apiLoginUrl: string;
  apiLogoutUrl: string;

  constructor(@Inject('BASE_URL') private baseUrl: string) {
    this.apiLoginUrl = baseUrl + 'api/account/login';
    this.apiLogoutUrl = baseUrl + 'api/account/logout';
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
