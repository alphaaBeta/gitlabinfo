import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../service/project/project.service';
import { RouterModule } from '@angular/router';
import { GroupService } from '../service/group/group.service';
import { IGroup } from '../group-management/models/group';
import { IProject } from './models/project';
import { StorageService } from '../service/storage/storage.service';
import { IReportedTimeGet } from './models/reportedTime';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.css']
})
export class ProjectManagementComponent implements OnInit {
  groups: IGroup[];
  selectedGroup: IGroup;
  projects: IProject[];
  errorMessage: string;
  reportedTimes: IReportedTimeGet[];

  projectViewEnabled = false;
  managementViewEnabled = false;
  ownerModeEnabled: boolean;

  constructor(private projectService: ProjectService, private groupService: GroupService, private storage: StorageService) { }

  ngOnInit() {
    this.groupService.getGroups(null, 10).subscribe(
      groups => {
        this.groups = groups;
      },
      error => this.errorMessage = <any>error
    );

    this.ownerModeEnabled = this.storage.isOwnerModeEnabled();
  }

  public getProjects(groupId: any) {
    this.projectViewEnabled = true;
    this.projects = null;
    this.managementViewEnabled = false;

    this.selectedGroup = this.groups.find(g => g.id === groupId);

    this.projectService.getProjects(groupId).subscribe(
      projects => {
        this.projects = projects;
        this.getProjectHours();
      },
      error => this.errorMessage = <any>error);
  }

  public getProjectHours() {
    this.reportedTimes = [];
    const projectIds = this.projects.map(p => p.id);
    projectIds.forEach(id => {
      this.projectService.getReportedTimes(id).subscribe(rt => {
        this.reportedTimes = this.reportedTimes.concat(rt);
      });
    });
  }

  public getTimeForUser(userId: number): number {
    const id = Number(userId);
    return this.reportedTimes.filter(e => e.userId === id).map(e => e.timeInHours).reduce((a, b) => a + b, 0);
  }

  public selectGroupManagement(groupId: any) {
    this.projectViewEnabled = false;

    this.selectedGroup = this.groups.find(g => g.id === groupId);
    this.managementViewEnabled = true;
  }
}
