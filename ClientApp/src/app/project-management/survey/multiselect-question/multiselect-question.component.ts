import { Component, OnInit, Input } from '@angular/core';
import { IMultiselectQuestion } from '../../../group-management/models/survey';
import { IMultiselectAnswer } from '../../../group-management/models/surveyAnswer';
import { IUser } from '../../../models/user';

@Component({
  selector: 'app-multiselect-question',
  templateUrl: './multiselect-question.component.html',
  styleUrls: ['./multiselect-question.component.css']
})
export class MultiselectQuestionComponent implements OnInit {
  @Input() question: IMultiselectQuestion;
  @Input() members: IUser[]
  answer: IMultiselectAnswer;
  testVar:any;

  constructor() { }

  ngOnInit() {
    this.answer = {
      questionId: this.question.id,
      answer: {
        choices: []
      }
    };
  }

  radioButtonChecked() {
    console.warn(this.answer.answer.choices);
  }

}
