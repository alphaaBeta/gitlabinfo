import { Component, OnInit } from '@angular/core';
import { IGroup } from '../models/group';
import { GroupService } from '../../service/group/group.service';

@Component({
  selector: 'app-group-owned-list',
  templateUrl: './group-owned-list.component.html',
  styleUrls: ['./group-owned-list.component.css']
})
export class GroupOwnedListComponent implements OnInit {
  groups: IGroup[];
  errorMessage: string;

  constructor(private groupService: GroupService) { }

  public getOwnedGroups() {
    this.groupService.getGroups(null, 50).subscribe(
      groups => {
        this.groups = groups;
      },
      error => this.errorMessage = <any>error
    );
  }

  ngOnInit() {
    this.getOwnedGroups();
  }

  updateGroupData(groupId: string) {
    const groupNmb = Number(groupId);
    if (!groupNmb) {
      return;
    } else {
      this.groupService.updateDbInfo(groupNmb).subscribe();
    }
  }

}
