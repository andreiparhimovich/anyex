import { Component, EventEmitter, Output } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import Question from 'src/app/common/Question';
import QuestionType from 'src/app/common/QuestionType';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-question-editor-container',
  templateUrl: './question-editor-container.component.html',
  styleUrls: ['./question-editor-container.component.css'],
  animations: [
    trigger('slideAnimation', [
      state('void', style({ transform: 'translateY(-100%)', opacity: 0 })),
      state('*', style({ transform: 'translateY(0)', opacity: 1 })),
      transition('void <=> *', [animate('500ms ease-in-out')])
    ])
  ]
})
export class QuestionEditorContainerComponent {

  @Output() questionAdded = new EventEmitter<Question>();

  QuestionType = QuestionType;
  questionTypes: { label: string, value: number }[];
  selectedQuestionType: { label: string, value: number };

  constructor() {
    this.questionTypes = [
      { label: GetQuestionTypeString(QuestionType.SingleChoice), value: QuestionType.SingleChoice },
      { label: GetQuestionTypeString(QuestionType.MultipleChoice), value: QuestionType.MultipleChoice },
      { label: GetQuestionTypeString(QuestionType.Rating), value: QuestionType.Rating },
      { label: GetQuestionTypeString(QuestionType.FreeText), value: QuestionType.FreeText }
    ];
    this.selectedQuestionType = this.questionTypes[0];
  }

  onSubmit(question: Question) {
    this.questionAdded.emit(question);
  }
}
