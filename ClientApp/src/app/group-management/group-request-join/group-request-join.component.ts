import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../service/group/group.service';

@Component({
  selector: 'app-group-request-join',
  templateUrl: './group-request-join.component.html',
  styleUrls: ['./group-request-join.component.css']
})
export class GroupRequestJoinComponent {
  groupId: number;
  submitted = false;

  constructor(private  groupService: GroupService) { }

  onSubmit() {
    this.submitted = true;
    this.groupService.putRequestToJoinGroup(this.groupId).subscribe();
  }

}
