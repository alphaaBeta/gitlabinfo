import { Component, OnInit } from '@angular/core';
import { IJoinRequest } from '../models/join-request';
import { GroupService } from '../../service/group/group.service';

@Component({
  selector: 'app-group-request-list',
  templateUrl: './group-request-list.component.html',
  styleUrls: ['./group-request-list.component.css']
})
export class GroupRequestListComponent implements OnInit {
  show: boolean = false;
  joinRequests: IJoinRequest[];
  errorMessage: string;

  constructor(private groupService: GroupService) { }

  private removeRow(idx: number) {
    this.joinRequests.splice(idx,1);
  }

  /**
   * getJoinRequests
   */
  public getJoinRequests() {
    this.groupService.getGetJoinRequestsForOwnedGroups(null).subscribe(
      joinRequests => {
        this.joinRequests = joinRequests;
      },
      error => this.errorMessage = <any>error
    );
  }

  /**
   * addUserToGroup
   */
  public addUserToGroup(userId: number, groupId: number) {
    this.groupService.putAddUserToGroup(groupId, userId).subscribe();
  }

  /**
   * removeUserJoinRequest
   */
  public removeUserJoinRequest(userId: number, groupId: number) {
    this.groupService.deleteRemoveUserJoinRequest(groupId, userId).subscribe();
  }

  ngOnInit() {
    this.getJoinRequests();
  }

}
