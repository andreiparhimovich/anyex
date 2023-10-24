import { Component, EventEmitter, Input, Output } from '@angular/core';
import { trigger, transition, style, animate, animation, useAnimation } from "@angular/animations";
import Question from 'src/app/common/Question';
import QuestionType from 'src/app/common/QuestionType';
import { AnswerViewModel } from 'src/app/common/AnswerViewModel';

const scaleIn = animation([
  style({ opacity: 0, transform: "scale(0.5)" }), // start state
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 1, transform: "scale(1)" })
  )
]);

@Component({
  selector: 'app-survey-answers-container',
  templateUrl: './survey-answers-container.component.html',
  styleUrls: ['./survey-answers-container.component.css'],
  animations: [
    trigger('answerItemAnimation', [
      transition("void => *", [useAnimation(scaleIn, { params: { time: '500ms' } })])
    ])
  ]
})
export class SurveyAnswersContainerComponent {
  @Input() isSurveySubmitting: boolean = false;
  @Input() questions!: Question[];
  @Output() onAnswersSubmit = new EventEmitter<AnswerViewModel[]>();

  QuestionType = QuestionType;

  answers: AnswerViewModel[] = [];

  currentItemIndex: number = 0;

  get isBackButtonDisabled() {
    //
    // check if the answer exists
    // 
    // const answerIsReady = this.answers[this.currentItemIndex] && this.answers[this.currentItemIndex]?.IsReadyToAnswer;
    // const answerIsFirst = this.currentItemIndex <= 0;

    // return answerIsFirst || !answerIsReady || this.isSurveySubmitting;
    const answerIsFirst = this.currentItemIndex <= 0;

    return answerIsFirst || this.isSurveySubmitting;
  }

  get isNextButtonDisabled() {
    return false;
    // return !this.answers[this.currentItemIndex]?.IsReadyToAnswer;
  }

  get isNextButtonDisplayed() {
    return this.currentItemIndex < this.questions.length - 1;
  }

  get isSubmitButtonDisabled() {
    return false;
    // var isQuestionLast = this.currentItemIndex === this.questions.length - 1;
    // var isAnswerReady = this.answers[this.currentItemIndex]?.IsReadyToAnswer;

    // return isQuestionLast && !isAnswerReady;
  }

  get isSubmitButtonDisplayed() {
    return this.currentItemIndex === this.questions.length - 1;
  }

  onPreviousClick() {
    if (this.currentItemIndex > 0) {
      this.currentItemIndex--;
    }
  }

  onNextClick() {
    if (this.currentItemIndex < this.questions.length - 1) {
      this.currentItemIndex++;
    }
  }

  onAnswerChange(answer: AnswerViewModel) {
    // we recieve an answer and have question index number
    // lets store in the array 
    this.answers[this.currentItemIndex] = answer;
  }

  submitAnswers() {
    this.onAnswersSubmit.emit(this.answers);
  }
}
