import { Component, Input } from '@angular/core';
import { QuestionResult } from 'src/app/common/SurveyResults';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-rating-question-result',
  templateUrl: './rating-question-result.component.html',
  styleUrls: ['./rating-question-result.component.css']
})
export class RatingQuestionResultComponent {
  @Input() questionResult!: QuestionResult;

  get questionType() { return GetQuestionTypeString(this.questionResult.questionType) }

  get averageRating() { return this.questionResult.averageRating }

  get maxRating() { return this.questionResult.maxRating }

  getRatingColor(): string {
    const redThreshold = this.maxRating / 3;
    const yellowThreshold = redThreshold * 2;

    if (this.averageRating > 0 && this.averageRating <= redThreshold)
      return "red";

    if (this.averageRating > redThreshold && this.averageRating <= yellowThreshold)
      return "yellow";

    return "green";
  }
}
