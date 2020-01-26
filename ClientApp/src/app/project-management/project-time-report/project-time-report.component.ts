import { Component, OnInit, Input } from '@angular/core';
import { IReportedTimePost } from '../models/reportedTime';
import { ProjectService } from '../../service/project/project.service';

@Component({
  selector: 'app-project-time-report',
  templateUrl: './project-time-report.component.html',
  styleUrls: ['./project-time-report.component.css']
})
export class ProjectTimeReportComponent implements OnInit {
  @Input() projectId: number;
  reportedTime: IReportedTimePost;

  submitted = false;
  success = false;
  failure = false;
  show = false;

  constructor(private projectService: ProjectService) {
    this.reportedTime = {
      projectId: null,
      date: null,
      description: '',
      timeInHours: 0,
      issueId: 0
    };
  }

  ngOnInit() {
  }

  submitHours() {
    this.submitted = true;
    this.success = false;
    this.failure = false;

    if (!this.reportedTime.date || !this.reportedTime.description || !this.reportedTime.timeInHours) {
      this.failure = true;
      this.submitted = false;
      return;
    }

    this.reportedTime.projectId = this.projectId;
    this.projectService.postReportTime(this.reportedTime).subscribe(success => {
      this.success = true;
    }, error => {
      this.failure = true;
      this.submitted = false;
    });
  }
}
