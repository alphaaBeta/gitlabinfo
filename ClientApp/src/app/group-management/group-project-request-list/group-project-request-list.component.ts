import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../service/group/group.service';
import { IProjectRequestGet } from '../../project-management/models/projectRequest';
import { ProjectService } from '../../service/project/project.service';
import { IUser } from '../../models/user';

@Component({
  selector: 'app-group-project-request-list',
  templateUrl: './group-project-request-list.component.html',
  styleUrls: ['./group-project-request-list.component.css']
})
export class GroupProjectRequestListComponent implements OnInit {
  show: boolean = false;
  projectRequests: IProjectRequestGet[];
  errorMessage: string;
  
  constructor(private projectService: ProjectService) { }

  private removeRow(idx: number) {
    this.projectRequests.splice(idx, 1);
  }

  /**
   * getJoinRequests
   */
  public getProjectRequests() {
    this.projectService.getProjectCreationRequests().subscribe(
      projectRequests => {
        this.projectRequests = projectRequests;
        this.projectRequests.forEach(pr => {
          let memberNames = (pr.members as IUser[]).map(m => m.name);
          pr.members = memberNames;
        });
      },
      error => this.errorMessage = <any>error
    );
  }

  /**
   * addUserToGroup
   */
  public approveProjectRequest(requestId: number) {
    this.projectService.approveProjectCreationRequest(requestId).subscribe();
  }

  /**
   * removeUserJoinRequest
   */
  public removeProjectRequest(requestId: number) {
    this.projectService.rejectProjectCreationRequest(requestId).subscribe();
  }

  ngOnInit() {
    this.getProjectRequests();
  }
}
