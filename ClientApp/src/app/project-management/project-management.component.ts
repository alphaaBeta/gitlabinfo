import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../service/project/project.service';
import { RouterModule } from '@angular/router';
import { GroupService } from '../service/group/group.service';
import { IGroup } from '../group-management/models/group';
import { IProject } from './models/project';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.css']
})
export class ProjectManagementComponent implements OnInit {
  groups: IGroup[];
  selectedGroupId: number;
  projects: IProject[];
  isSelectedGroupOwned = false;
  errorMessage: string;

  constructor(private projectService: ProjectService, private groupService: GroupService) { }

  ngOnInit() {
    this.groupService.getGroups(null, 10).subscribe(
      groups => {
        this.groups = groups;
      },
      error => this.errorMessage = <any>error
    );
  }

  public getProjects(groupId: any) {
    const group = this.groups.find(g => g.id === groupId);
    this.isSelectedGroupOwned = group.isOwner;
    this.selectedGroupId = groupId;
    this.projectService.getProjects(groupId).subscribe(
      projects => {
        this.projects = projects;
      },
      error => this.errorMessage = <any>error);
  }
}
