import { Component, OnInit, Input } from '@angular/core';
import { ITextQuestion } from '../../../group-management/models/survey';
import { ITextAnswer } from '../../../group-management/models/surveyAnswer';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.css']
})
export class TextQuestionComponent implements OnInit {
  @Input() question: ITextQuestion;
  answer: ITextAnswer;

  constructor() {
  }

  ngOnInit() {
    this.answer = {
      questionId: this.question.id,
      text: null
    };
  }

}
