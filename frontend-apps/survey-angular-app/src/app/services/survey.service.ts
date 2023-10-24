import { Injectable } from '@angular/core';
import Survey from '../common/Survey';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AnswerViewModel } from '../common/AnswerViewModel';

@Injectable({
  providedIn: 'root'
})

export class SurveyService {

  constructor(private http: HttpClient) { }

  createSurvey(survey: Survey) {

    const createSurveyApiEndpointUrl = `${environment.apiUrl}/survey/create`;

    return this.http.post(createSurveyApiEndpointUrl, survey);
  }

  getSurveyToSubmit(surveyId: string) {
    const getSurveyToSubmitApiEndpointUrl = `${environment.apiUrl}/survey/${surveyId}`;

    return this.http.get(getSurveyToSubmitApiEndpointUrl);
  }

  submitAnswers(surveyId: string, answers: AnswerViewModel[]) {
    const submitAnswersApiEndpointUrl = `${environment.apiUrl}/survey/${surveyId}/submit`;

    return this.http.post(submitAnswersApiEndpointUrl, answers);
  }

  getSurveyResults(surveyId: string, pinCode: string | null) {
    let getSurveyResultsApiEndpoint = `${environment.apiUrl}/surveyResults/${surveyId}`;

    if (pinCode) {
      getSurveyResultsApiEndpoint = getSurveyResultsApiEndpoint + `?pinCode=${pinCode}`;
    }

    return this.http.get(getSurveyResultsApiEndpoint);
  }

  checkIfSurveyRequiresPinCode(surveyId: string) {
    const checkIfSurveyRequiresPinCodeApiEndpoint = `${environment.apiUrl}/surveyResults/${surveyId}/requiresPin`;

    return this.http.get(checkIfSurveyRequiresPinCodeApiEndpoint);
  }
}