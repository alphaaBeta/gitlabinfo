import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { IGroup } from '../../group-management/models/group';
import { GroupService } from '../../service/group/group.service';
import { IGroupOptions, IGroupOptionsPost } from '../../group-management/models/groupOptions';
import { ISurvey } from '../../group-management/models/survey';

@Component({
  selector: 'app-project-group-management',
  templateUrl: './project-group-management.component.html',
  styleUrls: ['./project-group-management.component.css']
})
export class ProjectGroupManagementComponent implements OnInit, OnChanges {
  @Input() group: IGroup;
  newGroup: IGroup;
  existingSurveys: ISurvey[];
  surveyString: string;
  surveyId: number;

  submitted = false;
  success = false;
  failure = false;

  constructor(private groupService: GroupService) {
  }

  ngOnInit(): void {
    this.groupService.getSurveysFromOwnedGroup().subscribe(surveys => {
      this.existingSurveys = surveys;
    }, error => { });
    this.newGroup = this.group;
    this.surveyString = null;
    this.surveyId = null;
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    this.newGroup = this.group;
    this.surveyString = null;
    this.surveyId = null;
    this.submitted = false;
    this.success = false;
    this.failure = false;
  }

  submit() {
    this.success = false;
    this.failure = false;
    this.submitted = true;

    const groupOptionsPost = {
      groupId: Number(this.group.id),
      surveyString: this.surveyString,
      surveyId: this.surveyId,
      engagementPointsEnabled: this.group.options.engagementPointsEnabled,
      reportTimeEnabled: this.group.options.reportTimeEnabled,
      surveyEnabled: this.group.options.surveyEnabled,
      workDescriptionCommentsEnabled: this.group.options.workDescriptionCommentsEnabled,
      workDescriptionEnabled: this.group.options.workDescriptionEnabled,
      allowsProjectCreation: this.group.options.allowsProjectCreation
    } as IGroupOptionsPost;

    this.groupService.postGroupOptions(groupOptionsPost).subscribe(success => {
      this.success = true;
    }, error => {
      this.failure = true;
      this.submitted = false;
    });
  }

}
