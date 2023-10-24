import { Component, EventEmitter, Input, Output } from '@angular/core';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-single-choice-question-preview',
  templateUrl: './single-choice-question-preview.component.html',
  styleUrls: ['./single-choice-question-preview.component.css']
})
export class SingleChoiceQuestionPreviewComponent {
  @Input() question!: Question;
  @Output() delete: EventEmitter<Question> = new EventEmitter();

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  onDelete() {
    this.delete.emit(this.question);
  }
}
