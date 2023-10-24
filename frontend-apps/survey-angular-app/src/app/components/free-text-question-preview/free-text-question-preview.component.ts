import { Component, EventEmitter, Input, Output } from '@angular/core';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';


@Component({
  selector: 'app-free-text-question-preview',
  templateUrl: './free-text-question-preview.component.html',
  styleUrls: ['./free-text-question-preview.component.css']
})
export class CommentQuestionPreviewComponent {
  @Input() question!: Question;
  @Output() delete: EventEmitter<Question> = new EventEmitter();

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  onDelete() {
    this.delete.emit(this.question);
  }

}
