import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { StorageServiceModule } from 'angular-webstorage-service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AccountComponent } from './account/account.component';
import { GroupManagementComponent } from './group-management/group-management.component';
import { ProjectManagementComponent } from './project-management/project-management.component';
import { GroupOwnedListComponent } from './group-management/group-owned-list/group-owned-list.component';
import { GroupRequestJoinComponent } from './group-management/group-request-join/group-request-join.component';
import { GroupRequestListComponent } from './group-management/group-request-list/group-request-list.component';
import { GroupClaimOwnershipComponent } from './group-management/group-add-current-user/group-add-current-user.component';
import { ProjectRequestCreationComponent } from './project-management/project-request-creation/project-request-creation.component';
import { ProjectTimeReportComponent } from './project-management/project-time-report/project-time-report.component';
import { ProjectEngagementPointsComponent } from './project-management/project-engagement-points/project-engagement-points.component';
import { SurveyComponent } from './project-management/survey/survey.component';
import { MultiselectQuestionComponent } from './project-management/survey/multiselect-question/multiselect-question.component';
import { TextQuestionComponent } from './project-management/survey/text-question/text-question.component';
import { ProjectGroupManagementComponent } from './project-management/project-group-management/project-group-management.component';
import { ProjectWorkDescriptionPostComponent } from './project-management/project-work-description/project-work-description.component';
import { ProjectViewWorkDescriptionComponent } from './project-management/project-view-work-description/project-view-work-description.component';
import { GroupProjectRequestListComponent } from './group-management/group-project-request-list/group-project-request-list.component';

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
    GroupClaimOwnershipComponent,
    ProjectRequestCreationComponent,
    ProjectTimeReportComponent,
    ProjectEngagementPointsComponent,
    SurveyComponent,
    MultiselectQuestionComponent,
    TextQuestionComponent,
    ProjectGroupManagementComponent,
    ProjectWorkDescriptionPostComponent,
    ProjectViewWorkDescriptionComponent,
    GroupProjectRequestListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'groups', component: GroupManagementComponent, pathMatch: 'full' },
      { path: 'projects', component: ProjectManagementComponent, pathMatch: 'full' },
      { path: 'projects/request/:groupId', component: ProjectRequestCreationComponent },
      { path: 'account', component: AccountComponent, pathMatch: 'full' }
    ]),
    StorageServiceModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
