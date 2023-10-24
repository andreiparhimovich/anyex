import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime } from 'rxjs';
import { AnswerViewModel } from 'src/app/common/AnswerViewModel';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-free-text-answer',
  templateUrl: './free-text-answer.component.html',
  styleUrls: ['./free-text-answer.component.css']
})
export class FreeTextAnswerComponent implements OnInit {
  @Input() question!: Question;
  @Input() answer: AnswerViewModel | undefined;
  @Output() answerChange = new EventEmitter<AnswerViewModel>();

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  get answerText() { return this.freeTextFormGroup.get('answerText') }

  freeTextFormGroup!: FormGroup;

  ngOnInit() {
    this.freeTextFormGroup = this.buildForm();

    var answerTextControl = this.freeTextFormGroup.get('answerText');

    answerTextControl?.valueChanges
      .pipe(debounceTime(500))
      .subscribe(value => this.onFormValueChange(value));
  }

  buildForm() {

    const value = this.answer?.Text ?? '';

    return new FormGroup({
      answerText: new FormControl(value, [Validators.required, Validators.maxLength(1000)])
    });
  }

  onFormValueChange(value: string) {

    const answer: AnswerViewModel = {
      QuestionIndexNumber: this.question.indexNumber,
      QuestionType: this.question.questionType,
      SelectedOption: undefined,
      SelectedOptions: undefined,
      Text: value,
      Rating: undefined,
      IsReadyToAnswer: value !== '' && this.freeTextFormGroup.valid
    };

    this.answerChange.emit(answer);
  }
}
