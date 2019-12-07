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
  errorMessage: string;

  constructor(private projectService: ProjectService, private groupService: GroupService) { }

  ngOnInit() {
    //TODO: NOT ONLY owned groups!!!!!!
    this.groupService.getGetOwnedGroups(null).subscribe(
      groups => {
        this.groups = groups;
      },
      error => this.errorMessage = <any>error
    );
  }

  public getProjects(groupId: any) {
    this.selectedGroupId = groupId;
    this.projectService.getProjects(groupId).subscribe(
      projects => {
        this.projects = projects;
      },
      error => this.errorMessage = <any>error);
  }

}
