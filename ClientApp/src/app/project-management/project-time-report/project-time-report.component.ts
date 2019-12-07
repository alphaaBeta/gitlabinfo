import { Component, OnInit, Input } from '@angular/core';
import { IReportedTime } from '../models/reportedTime';
import { ProjectService } from '../../service/project/project.service';

@Component({
  selector: 'app-project-time-report',
  templateUrl: './project-time-report.component.html',
  styleUrls: ['./project-time-report.component.css']
})
export class ProjectTimeReportComponent implements OnInit {
  @Input() projectId: number;
  reportedTime: IReportedTime;
  submitted = false;

  constructor(private projectService: ProjectService) {
    this.reportedTime = {
      projectId: null,
      date: null,
      description: '',
      timeInHours: 0
    };
  }

  ngOnInit() {
  }

  submitHours() {
    this.reportedTime.projectId = this.projectId;
    this.submitted = true;
    console.log(this.reportedTime);
    this.projectService.postReportTime(this.reportedTime).subscribe();
  }
}
