import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../service/group/group.service';

@Component({
  selector: 'app-group-add-current-user',
  templateUrl: './group-add-current-user.component.html',
  styleUrls: ['./group-add-current-user.component.css']
})
export class GroupClaimOwnershipComponent {
  groupId: number;
  submitted = false;
  success = true;

  constructor(private  groupService: GroupService) { }

  onSubmit() {
    this.success = true;
    this.groupService.putAddCurrentUserAsGroupOwner(this.groupId, undefined).subscribe(success => {
      this.success = true;
      this.submitted = true;
    }, error => {
      this.success = false;
      this.submitted = true;
    });
  }
}
