import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../service/project/project.service';

@Component({
  selector: 'app-project-work-description',
  templateUrl: './project-work-description.component.html',
  styleUrls: ['./project-work-description.component.css']
})
export class ProjectWorkDescriptionPostComponent implements OnInit {
  @Input() projectId: number;
  workDescription: string;

  submitted = false;
  success = false;
  failure = false;

  constructor(private projectService: ProjectService) { }

  ngOnInit() {
  }

  submit() {
    this.submitted = true;
    this.success = false;
    this.failure = false;

    this.projectService.postWorkDescription({
      description: this.workDescription,
      projectId: this.projectId
    }).subscribe(success => {
      this.success = true;
    }, error => {
      this.failure = true;
      this.submitted = false;
    });
  }

}
