import { Component, OnInit } from '@angular/core';
import { IJoinRequest } from '../join-request';
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

  public getJoinRequests() {
    this.groupService.getGetJoinRequestsForOwnedGroups(null).subscribe(
      joinRequests => {
        this.joinRequests = joinRequests;
      },
      error => this.errorMessage = <any>error
    );
  }

  ngOnInit() {
    this.getJoinRequests();
  }

}
