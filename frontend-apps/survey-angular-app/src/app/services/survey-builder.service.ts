import { Injectable } from '@angular/core';
import Survey, { SurveyResultsAccessType } from '../common/Survey';
import Question from '../common/Question';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SurveyBuilderService {

  survey: Survey | undefined;

  currentStep: SurveyBuilderSteps;

  constructor() {
    this.currentStep = SurveyBuilderSteps.NotStarted;
  }

  getCurrentStep(): SurveyBuilderSteps {
    return this.currentStep;
  };

  setCurrentStep(step: SurveyBuilderSteps) {
    this.currentStep = step;
  }

  getInstance(): Survey | undefined {
    return this.survey;
  }

  reset() {

    const currentMonth = new Date().getMonth();
    const nextMonth = (currentMonth === 11) ? 0 : currentMonth + 1;
    const expirationDate = new Date();
    expirationDate.setMonth(nextMonth);

    this.survey = {
      id: '',
      title: '',
      questions: [],
      surveyUrl: '',
      surveyResultsUrl: '',
      description: '',
      useExpirationDate: false,
      expirationDateUtc: expirationDate,
      resultsAccessType: SurveyResultsAccessType.Public,
      pinCode: ''
    }

    this.currentStep = SurveyBuilderSteps.NotStarted;
  }

  addTitle(title: string) {
    if (this.survey) {
      this.survey.title = title;
    }
  }

  addQuestions(questions: Question[]) {
    if (this.survey) {
      this.survey.questions = questions;
    }
  }

  addSettings(description: string, useExpirationDate: boolean, expirationDate: Date, resultsAccessType: SurveyResultsAccessType, pinCode: string) {
    if (this.survey) {
      this.survey.description = description;
      this.survey.useExpirationDate = useExpirationDate;
      this.survey.expirationDateUtc = expirationDate;
      this.survey.resultsAccessType = resultsAccessType;
      this.survey.pinCode = pinCode;
    }
  }

  addLinks(surveyUrl: string, surveyResultsUrl: string) {
    if (this.survey) {
      this.survey.surveyUrl = environment.host + surveyUrl;
      this.survey.surveyResultsUrl = environment.host + surveyResultsUrl;
    }
  }

  setId(id: string) {
    if (this.survey) {
      this.survey.id = id;
    }
  }

  getResult(): Survey {
    if (this.survey) {
      const survey = this.survey;

      this.reset();

      return survey;
    }

    throw new Error("survey is not initialized");
  }
}

export enum SurveyBuilderSteps {
  NotStarted = 0,
  Questions = 1,
  Settings = 2,
  Links = 3
}
