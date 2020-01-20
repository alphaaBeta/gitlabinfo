import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../service/project/project.service';
import { RouterModule } from '@angular/router';
import { GroupService } from '../service/group/group.service';
import { IGroup } from '../group-management/models/group';
import { IProject } from './models/project';
import { StorageService } from '../service/storage/storage.service';

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
      },
      error => this.errorMessage = <any>error);
  }

  public selectGroupManagement(groupId: any) {
    this.projectViewEnabled = false;

    this.selectedGroup = this.groups.find(g => g.id === groupId);
    this.managementViewEnabled = true;
  }
}
