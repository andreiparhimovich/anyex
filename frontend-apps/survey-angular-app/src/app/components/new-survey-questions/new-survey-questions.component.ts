import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { trigger, transition, style, animate, animation, useAnimation } from "@angular/animations";
import { Router } from '@angular/router';
import Question from '../../common/Question';
import { SurveyBuilderService, SurveyBuilderSteps } from '../../services/survey-builder.service';
import { GetSurveySteps } from '../../common/Utils';
import { MessageService } from 'primeng/api';

const scaleIn = animation([
  style({ opacity: 0, transform: "scale(0.5)" }), // start state
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 1, transform: "scale(1)" })
  )
]);

const scaleOut = animation([
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 0, transform: "scale(0.5)" })
  )
]);

@Component({
  selector: 'app-new-survey-questions',
  templateUrl: './new-survey-questions.component.html',
  styleUrls: ['./new-survey-questions.component.css'],
  animations: [
    trigger('appearanceAnimation', [
      transition("void => *", [useAnimation(scaleIn, { params: { time: '500ms' } })]),
      transition("* => void", [useAnimation(scaleOut, { params: { time: '500ms' } })]),
    ])
  ]
})
export class NewSurveyQuestionsComponent {

  maxNumberOfQuestions = 10;

  newSurveyFormGroup: FormGroup;

  surveySteps: any[] = GetSurveySteps();

  questions: Question[] = [];

  get surveyTitle() { return this.newSurveyFormGroup.get('surveyTitle') }

  get isAnyQuestion() { return this.questions.length > 0 }

  get isMaxNumberOfQuestions() { return this.questions.length == this.maxNumberOfQuestions }

  get isMoreThanMaxNumberOfQuestions() { return this.questions.length > this.maxNumberOfQuestions }

  isQuestionJustAdded: boolean = false;
  get isQuestionEditorDisplayed() { return !this.isQuestionJustAdded || this.questions.length <= 0 };
  get isAddQuestionButtonDisplayed() { return this.isQuestionJustAdded }

  constructor(
    private router: Router,
    private surveyBuilder: SurveyBuilderService,
    private messageService: MessageService) {

    //
    // if survey builder is not started then redirect to '/home'
    //
    const surveyBuilderCurrentStep = this.surveyBuilder.getCurrentStep();

    if (surveyBuilderCurrentStep == SurveyBuilderSteps.NotStarted) {
      this.router.navigate(['/']);
    }

    //
    // if survey builder is on Links step then reset survey and redirect to '/'
    //
    if (surveyBuilderCurrentStep == SurveyBuilderSteps.Links) {
      this.surveyBuilder.reset();
      this.router.navigate(['/']);
    }

    //
    // proceed with survey instance from the builder. Cannot be null in all the cases
    //
    const survey = this.surveyBuilder.getInstance();

    if (survey) {
      //
      // take a copy of questions
      //
      this.questions = survey.questions.slice();

      this.newSurveyFormGroup = new FormGroup({
        surveyTitle: new FormControl(survey.title, [Validators.maxLength(250)])
      });
    } else {
      throw new Error("Survey cannot be null");
    }
  }

  onGoNext(event: SubmitEvent) {

    event.stopPropagation();

    this.surveyBuilder.addTitle(this.surveyTitle?.value);
    this.surveyBuilder.addQuestions(this.questions);

    //
    // if the current survey builder step is 'Questions' update to the next 'Settings' and go to '/settings'
    //
    const surveyBuilderCurrentStep = this.surveyBuilder.getCurrentStep();

    if (surveyBuilderCurrentStep == SurveyBuilderSteps.Questions) {
      this.surveyBuilder.setCurrentStep(SurveyBuilderSteps.Settings);
    }

    this.router.navigate(['/new/settings']);
  }

  onQuestionAdded(question: Question) {
    if (this.isMaxNumberOfQuestions) {
      this.messageService.add(
        {
          severity: "info",
          summary: "Can't add a new question",
          detail: `Max number of questions is ${this.maxNumberOfQuestions}`
        });
    } else {
      question.indexNumber = this.questions.length + 1;
      this.questions.push(question);
    }

    this.isQuestionJustAdded = true;
  }

  onQuestionDelete(question: Question) {
    const index = this.questions.indexOf(question);

    if (index != -1) {
      this.questions.splice(index, 1);
    }

    // reassign index numbers
    this.reassignIndexNumbers();
  }

  private reassignIndexNumbers() {
    for (let index = 0; index < this.questions.length; index++) {
      const element = this.questions[index];
      element.indexNumber = index + 1
    }
  }

  addQuestionClick() {
    this.isQuestionJustAdded = false;
  }
}
