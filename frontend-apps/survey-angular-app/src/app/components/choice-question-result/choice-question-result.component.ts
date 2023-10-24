import { Component, Input } from '@angular/core';
import { QuestionResult } from 'src/app/common/SurveyResults';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-choice-question-result',
  templateUrl: './choice-question-result.component.html',
  styleUrls: ['./choice-question-result.component.css']
})
export class ChoiceQuestionResultComponent {
  @Input() questionResult!: QuestionResult;

  get questionType() { return GetQuestionTypeString(this.questionResult.questionType) }
}
