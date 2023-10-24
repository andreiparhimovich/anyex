import { Component, EventEmitter, Input, Output, SimpleChange, SimpleChanges } from '@angular/core';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-rating-question-preview',
  templateUrl: './rating-question-preview.component.html',
  styleUrls: ['./rating-question-preview.component.css']
})
export class RatingQuestionPreviewComponent {
  @Input() question!: Question;
  @Output() delete: EventEmitter<Question> = new EventEmitter();

  maxRating?: number;

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  ngOnChanges(changes: SimpleChanges) {
    const question = changes["question"].currentValue as Question;
    this.maxRating = question.maxRating;
  }

  onDelete() {
    this.delete.emit(this.question);
  }
}
