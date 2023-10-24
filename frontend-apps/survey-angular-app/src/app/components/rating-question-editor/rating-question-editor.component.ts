import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import Question from 'src/app/common/Question';
import QuestionType from 'src/app/common/QuestionType';

@Component({
  selector: 'app-rating-question-editor',
  templateUrl: './rating-question-editor.component.html',
  styleUrls: ['./rating-question-editor.component.css']
})
export class RatingQuestionEditorComponent {

  @Output() submit = new EventEmitter<Question>();

  ratingQuestionFormGroup: FormGroup;

  get questionText() { return this.ratingQuestionFormGroup.get('questionText') }

  constructor() {
    this.ratingQuestionFormGroup = this.buildForm();
  }

  buildForm() {
    return new FormGroup({
      questionText: new FormControl('', [Validators.required, Validators.maxLength(250)]),
      maxRating: new FormControl(1)
    });
  }

  onSubmit(event: SubmitEvent) {

    event.stopPropagation();

    const question: Question = {
      indexNumber: 1,
      questionType: QuestionType.Rating,
      text: this.ratingQuestionFormGroup.value.questionText ?? '',
      maxRating: this.ratingQuestionFormGroup.value.maxRating ?? 2,
      options: []
    }

    this.submit.emit(question);

    this.ratingQuestionFormGroup.reset();

    this.ratingQuestionFormGroup = this.buildForm();
  }
}
