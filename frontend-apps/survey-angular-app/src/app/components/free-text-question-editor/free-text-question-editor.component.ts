import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import Question from 'src/app/common/Question';
import QuestionType from 'src/app/common/QuestionType';

@Component({
  selector: 'app-free-text-question-editor',
  templateUrl: './free-text-question-editor.component.html',
  styleUrls: ['./free-text-question-editor.component.css']
})
export class FreeTextQuestionEditorComponent {

  @Output() submit = new EventEmitter<Question>();

  freeTextQuestionFormGroup = new FormGroup({
    questionText: new FormControl('', [Validators.required, Validators.maxLength(250)])
  });

  get questionText() { return this.freeTextQuestionFormGroup.get('questionText') }

  onSubmit(event: SubmitEvent) {

    event.stopPropagation();

    const question: Question = {
      indexNumber: 1,
      questionType: QuestionType.FreeText,
      text: this.freeTextQuestionFormGroup.value.questionText ?? '',
      maxRating: 0,
      options: []
    }

    this.submit.emit(question);

    this.freeTextQuestionFormGroup.reset();
  }
}
