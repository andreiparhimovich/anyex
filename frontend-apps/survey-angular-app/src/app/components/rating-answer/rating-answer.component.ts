import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AnswerViewModel } from 'src/app/common/AnswerViewModel';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-rating-answer',
  templateUrl: './rating-answer.component.html',
  styleUrls: ['./rating-answer.component.css']
})
export class RatingAnswerComponent implements OnInit {
  @Input() question!: Question;
  @Input() answer: AnswerViewModel | undefined;
  @Output() answerChange = new EventEmitter<AnswerViewModel>();

  get questionType() { return GetQuestionTypeString(this.question.questionType) }
  get questionMaxRating() { return this.question.maxRating ?? 10 }

  ratingAnswerFormGroup!: FormGroup;

  ngOnInit(): void {
    this.ratingAnswerFormGroup = this.buildForm();

    var ratingControl = this.ratingAnswerFormGroup.get('rating');

    ratingControl?.valueChanges.subscribe(value => this.onFormValueChange(value));
  }

  buildForm() {
    const value = this.answer?.Rating ?? 1;

    return new FormGroup({
      rating: new FormControl(value)
    });
  }

  onFormValueChange(value: any) {

    const answer: AnswerViewModel = {
      QuestionIndexNumber: this.question.indexNumber,
      QuestionType: this.question.questionType,
      SelectedOption: undefined,
      SelectedOptions: undefined,
      Text: undefined,
      Rating: value,
      IsReadyToAnswer: value > 0
    };

    this.answerChange.emit(answer);
  }
}
