import { Component, Input } from '@angular/core';
import { QuestionResult } from 'src/app/common/SurveyResults';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-free-text-question-result',
  templateUrl: './free-text-question-result.component.html',
  styleUrls: ['./free-text-question-result.component.css']
})
export class FreeTextQuestionResultComponent {
  @Input() questionResult!: QuestionResult;

  get questionType() { return GetQuestionTypeString(this.questionResult.questionType) }

  get answers() { return this.questionResult.answerTexts; }
}

interface FreeTextQuestionViewModel {
  index: number;
  text: string;
}
