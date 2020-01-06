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

  clicked: boolean;
  submitted: boolean;
  invalidDataString: string;

  constructor(private projectService: ProjectService) { }

  ngOnInit() {
  }

  submit() {
    this.invalidDataString = null;
    this.clicked = true;

    this.projectService.postWorkDescription({
      description: this.workDescription,
      projectId: this.projectId
    }).subscribe(success => {
      this.submitted = true;
      this.clicked = false;
    }, error => {
      this.invalidDataString = 'There was an error during the request';
      this.clicked = false;
    });
  }

}
