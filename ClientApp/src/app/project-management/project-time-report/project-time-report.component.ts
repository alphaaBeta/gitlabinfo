import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-project-time-report',
  templateUrl: './project-time-report.component.html',
  styleUrls: ['./project-time-report.component.css']
})
export class ProjectTimeReportComponent implements OnInit {
  @Input() projectId: number;
  hours: number;
  date: Date;
  description: string;

  constructor() { }

  ngOnInit() {
  }

}
