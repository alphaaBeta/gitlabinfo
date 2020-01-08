import { Component, OnInit, Input, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { GroupService } from '../../service/group/group.service';
import { ISurvey } from '../../group-management/models/survey';
import { ISurveyAnswer } from '../../group-management/models/surveyAnswer';
import { IUser } from '../../models/user';
import { MultiselectQuestionComponent } from './multiselect-question/multiselect-question.component';
import { TextQuestionComponent } from './text-question/text-question.component';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {
  @Input() groupId: number;
  @Input() projectId: number;
  @Input() projectMembers: IUser[];
  @ViewChildren(MultiselectQuestionComponent) multiselectQuestions: QueryList<MultiselectQuestionComponent>;
  @ViewChildren(TextQuestionComponent) textQuestions: QueryList<TextQuestionComponent>;

  survey: ISurvey;
  surveyAnswer: ISurveyAnswer;

  submitted = false;
  success = false;
  failure = false;
  showSurvey = false;

  constructor(private groupService: GroupService) { }

  ngOnInit() {
    this.groupService.getAvailableSurveys(this.groupId).subscribe(surveys => {
      if (surveys) {
        this.survey = surveys[0];
      }
    });
  }

  submitSurvey() {
    this.submitted = true;
    this.success = false;
    this.failure = false;

    this.surveyAnswer = {
      surveyId: this.survey.surveyId,
      projectId: this.projectId,
      multiselectAnswers: [],
      textAnswers: []
    };

    this.multiselectQuestions.forEach(q => this.surveyAnswer.multiselectAnswers.push(q.answer));
    this.textQuestions.forEach(q => this.surveyAnswer.textAnswers.push(q.answer));
    this.groupService.postSurveyAnswer(this.surveyAnswer).subscribe(success => {
      this.success = true;
    }, error => {
      this.failure = true;
      this.submitted = false;
    });
  }

}
