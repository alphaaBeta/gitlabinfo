import { Component, OnInit } from '@angular/core';
import { GroupService } from '../service/group/group.service';
import { StorageService } from '../service/storage/storage.service';

@Component({
  selector: 'app-group-management',
  templateUrl: './group-management.component.html',
  styleUrls: ['./group-management.component.css']
})
export class GroupManagementComponent implements OnInit {
  ownerModeEnabled: boolean;

  constructor(private localStorage: StorageService) { }

  ngOnInit() {
    this.ownerModeEnabled = this.localStorage.isOwnerModeEnabled();
  }

}
