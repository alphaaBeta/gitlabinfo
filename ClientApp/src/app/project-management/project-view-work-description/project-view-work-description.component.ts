import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../service/project/project.service';
import { IWorkDescriptionGet } from '../models/workDescription';

@Component({
  selector: 'app-project-view-work-description',
  templateUrl: './project-view-work-description.component.html',
  styleUrls: ['./project-view-work-description.component.css']
})
export class ProjectViewWorkDescriptionComponent implements OnInit {
  @Input() projectId: number;
  workDescriptions: IWorkDescriptionGet[];
  workDescriptionComments: string[];

  submitted: boolean[];
  show = false;

  constructor(private projectService: ProjectService) {
    this.workDescriptionComments = [];
    this.submitted = [];
  }

  ngOnInit() {
    this.projectService.getWorkDescriptions(this.projectId).subscribe(workDescriptions => {
      this.workDescriptions = workDescriptions;
    }, error => {
      console.error(error);
    });
  }

  submit(workDescriptionId: number, comment: string, index: number) {
    this.submitted[index] = true;
    this.projectService.putWorkDescriptionComment(workDescriptionId, comment).subscribe();
  }

}
