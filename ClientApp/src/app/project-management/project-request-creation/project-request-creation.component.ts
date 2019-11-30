import { Component, OnInit } from '@angular/core';
import { IProjectRequest } from '../models/projectRequest';
import { ProjectService } from '../../service/project/project.service';
import { switchMap } from 'rxjs/operators';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-project-request-creation',
  templateUrl: './project-request-creation.component.html',
  styleUrls: ['./project-request-creation.component.css']
})
export class ProjectRequestCreationComponent implements OnInit {
  projectRequest: IProjectRequest;
  member1: string;
  member2: string;
  member3: string;
  member4: string;
  member5: string;
  parentGroupId: number;
  submitted = false;

  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(paramMap =>
      this.parentGroupId = Number(paramMap.get('groupId'))
    );

    this.projectRequest = {
      member_emails: [],
      parent_group_id: null,
      project: {
      }
    };
  }

  onSubmit() {
    if (this.member1 != null) {
      this.projectRequest.member_emails.push(this.member1);
    }
    if (this.member2 != null) {
      this.projectRequest.member_emails.push(this.member2);
    }
    if (this.member3 != null) {
      this.projectRequest.member_emails.push(this.member3);
    }
    if (this.member4 != null) {
      this.projectRequest.member_emails.push(this.member4);
    }
    if (this.member5 != null) {
      this.projectRequest.member_emails.push(this.member5);
    }

    this.projectRequest.parent_group_id = this.parentGroupId;

    this.submitted = true;
    console.log(JSON.stringify(this.projectRequest));
    this.projectService.postRequestProjectCreation(this.projectRequest).subscribe();
  }

}
