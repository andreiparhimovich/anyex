import { Component, EventEmitter, Input, Output } from '@angular/core';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-multiple-choice-question-preview',
  templateUrl: './multiple-choice-question-preview.component.html',
  styleUrls: ['./multiple-choice-question-preview.component.css']
})
export class MultipleChoiceQuestionPreviewComponent {
  @Input() question!: Question;
  @Output() delete: EventEmitter<Question> = new EventEmitter();

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  onDelete() {
    this.delete.emit(this.question);
  }
}
