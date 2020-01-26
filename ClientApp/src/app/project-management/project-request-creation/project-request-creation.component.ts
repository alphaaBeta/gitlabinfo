import { Component, OnInit } from '@angular/core';
import { IProjectRequestPut } from '../models/projectRequest';
import { ProjectService } from '../../service/project/project.service';
import { switchMap } from 'rxjs/operators';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-project-request-creation',
  templateUrl: './project-request-creation.component.html',
  styleUrls: ['./project-request-creation.component.css']
})
export class ProjectRequestCreationComponent implements OnInit {
  name: string;
  description: string;
  parentGroupId: number;
  member_emails = <Array<string>>['', ''];

  submitted = false;
  success = false;
  failure = false;

  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(paramMap => {
      this.parentGroupId = Number(paramMap.get('groupId'));
    }
    );
  }

  onSubmit() {
    this.success = false;
    this.failure = false;
    this.submitted = true;

    const projectRequest = <IProjectRequestPut>{
      description: this.description,
      member_emails: this.member_emails.filter(el => el.length > 0),
      name: this.name,
      parent_group_id: this.parentGroupId
    };

    this.projectService.postRequestProjectCreation(projectRequest).subscribe(success => {
      this.success = true;
    }, error => {
      this.failure = true;
      this.submitted = false;
    });
  }

  addMember() {
    this.member_emails.push('');
  }

}
