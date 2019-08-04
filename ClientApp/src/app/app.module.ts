import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AccountComponent } from './account/account.component';
import { GroupManagementComponent } from './group-management/group-management.component';
import { ProjectManagementComponent } from './project-management/project-management.component';
import { GroupOwnedListComponent } from './group-management/group-owned-list/group-owned-list.component';
import { GroupRequestJoinComponent } from './group-management/group-request-join/group-request-join.component';
import { GroupRequestListComponent } from './group-management/group-request-list/group-request-list.component';
import { GroupAddUserComponent } from './group-management/group-add-user/group-add-user.component';
import { GroupAddCurrentUserComponent } from './group-management/group-add-current-user/group-add-current-user.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AccountComponent,
    GroupManagementComponent,
    ProjectManagementComponent,
    GroupOwnedListComponent,
    GroupRequestJoinComponent,
    GroupRequestListComponent,
    GroupAddUserComponent,
    GroupAddCurrentUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'groups', component: GroupManagementComponent },
      { path: 'projects', component: ProjectManagementComponent },
      { path: 'account', component: AccountComponent, pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
