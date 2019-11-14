import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../service/project/project.service';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.css']
})
export class ProjectManagementComponent implements OnInit {

  constructor(private projectService: ProjectService) { }

  ngOnInit() {
    this.projectService.postRequestProjectCreation({
      member_emails: [
        'abc@bca.pl',
        'abdsad@dfsaf'
      ],
      project: {
        name: 'abc',
        description: 'bca'
      },
      parent_group_id: 1
    }).subscribe();
  }

}
