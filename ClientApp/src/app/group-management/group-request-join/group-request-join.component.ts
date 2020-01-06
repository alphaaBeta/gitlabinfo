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
  success = true;

  constructor(private  groupService: GroupService) { }

  onSubmit() {
    this.success = true;
    this.groupService.putRequestToJoinGroup(this.groupId).subscribe(success => {
      this.success = true;
      this.submitted = true;
    }, error => {
      this.success = false;
      this.submitted = true;
    });
  }

}
