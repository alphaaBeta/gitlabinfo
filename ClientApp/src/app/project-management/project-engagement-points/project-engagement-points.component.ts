import { Component, OnInit, Input } from '@angular/core';
import { IUser } from '../../models/user';
import { ProjectService } from '../../service/project/project.service';

@Component({
  selector: 'app-project-engagement-points',
  templateUrl: './project-engagement-points.component.html',
  styleUrls: ['./project-engagement-points.component.css']
})
export class ProjectEngagementPointsComponent implements OnInit {
  @Input() members: IUser[];
  @Input() remainingPoints: number;
  @Input() projectId: number;
  grantedPoints: number[];

  submitted = false;
  success = false;
  failure = false;

  constructor(private projectService: ProjectService) {
    this.grantedPoints = [];
  }

  ngOnInit() {
  }

  submitPoints() {
    this.success = false;
    this.failure = false;
    this.submitted = true;

    for (const i in this.members) {
      if (this.members.hasOwnProperty(i)) {
        const member = this.members[i];
        if (!this.grantedPoints[i]) {
          this.grantedPoints[i] = 0;
        }
        this.projectService.submitEngagementPoints({
          points: this.grantedPoints[i],
          receivingUser: {
            id: member.id,
            name: member.name,
            email: null
          },
          projectId: this.projectId
        }).subscribe(success => {
          this.success = true;
        }, error => {
          this.failure = true;
          this.submitted = false;
        });
      }
    }
  }

  sumArray(): number {
    return this.grantedPoints.reduce(function (a, b) { return Number(a) + Number(b); }, 0);
  }

}
