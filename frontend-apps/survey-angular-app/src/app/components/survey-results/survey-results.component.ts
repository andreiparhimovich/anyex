import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';
import { MessageService } from 'primeng/api';
import QuestionType from 'src/app/common/QuestionType';
import { SurveyResults } from 'src/app/common/SurveyResults';
import { environment } from 'src/environments/environment';
import { SurveyService } from 'src/app/services/survey.service';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.css']
})
export class SurveyResultsComponent {

  private surveyResultsHubConnection: HubConnection | undefined;

  private surveyId: string;

  QuestionType = QuestionType;

  surveyResults!: SurveyResults;

  isSurveyResultsLoading: boolean = false;

  isPinCodeProvided: boolean = false;
  isPinCodeRequired: boolean = false;

  isPinCodeVerifying: boolean = false;
  isPinCodeInvalid: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private surveyService: SurveyService,
    private messageService: MessageService) {

    this.isSurveyResultsLoading = true;

    this.surveyId = this.route.snapshot.paramMap.get('id')!;

    //
    // Try to get PIN code for the current survey from local storage
    //
    const pinCode = localStorage.getItem(this.surveyId);

    //
    // Try to get results.
    //
    this.surveyService.getSurveyResults(this.surveyId, pinCode)
      .subscribe({
        next: (response) => {

          this.surveyResults = response as SurveyResults;

          //
          // once we successfully obtain survey results
          //    subscribe to continue getting results 
          //
          this.surveyResultsSubscribe(this.surveyId, pinCode);

          //
          // If PIN code is NULL it means PIN code not required
          //    If PIN is not NULL it means PIN required and was provided
          //
          if (pinCode == null) {
            this.isPinCodeRequired = false;
          } else {
            this.isPinCodeRequired = true;
            this.isPinCodeProvided = true;
          }

          this.isSurveyResultsLoading = false;
        },
        error: (error) => {

          //
          // check if not authorized. it means pin code is required
          //
          if (error.status == 401) {
            this.isPinCodeProvided = false;
            this.isPinCodeRequired = true;
            this.isSurveyResultsLoading = false;
            return;
          }

          //
          // redirect if not found
          //
          if (error.status == 404) {
            this.router.navigate(['/notfound']);
            return;
          }

          //
          // if something else just display an error message
          //
          this.messageService.add({ severity: "error", summary: "Unknown error" });
          this.isSurveyResultsLoading = false;
        }
      })
  }

  surveyResultsSubscribe(surveyId: string, pinCode: string | null) {
    //
    // initialize connection to survey results hub
    //
    const hubUrl = `${environment.hubsUrl}/surveyResults`;

    this.surveyResultsHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    //
    // subscribe on server events
    //
    this.surveyResultsHubConnection.on('SurveySubmitted', (surveyResults: SurveyResults) => {
      this.surveyResults = surveyResults;
    })

    this.surveyResultsHubConnection
      .start()
      .then(() => {

        //
        // call to associate this connection with certain survey
        //
        this.surveyResultsHubConnection!
          .invoke("AssociateConnectionWithSurveyId", surveyId, pinCode)
          .catch((error) => console.log(error));

      })
      .catch((error) => console.log(error));
  }


  verifyPinCodeClick(pinCode: string) {
    this.isPinCodeVerifying = true;

    this.surveyService.getSurveyResults(this.surveyId, pinCode)
      .subscribe({
        next: (response) => {

          this.surveyResults = response as SurveyResults;

          //
          // once we successfully obtain survey results
          //    subscribe to continue getting results 
          //
          this.surveyResultsSubscribe(this.surveyId, pinCode);

          //
          // save current PIN for the survey in local storage to avoid checking pin code after page reload
          //
          localStorage.setItem(this.surveyId, pinCode);


          this.isPinCodeProvided = true;
          this.isPinCodeInvalid = false;

          this.isPinCodeVerifying = false;
        },
        error: (error) => {

          //
          // check if not authorized. it means pin code is required
          //
          if (error.status == 401) {
            this.isPinCodeProvided = false;
            this.isPinCodeInvalid = true;

            this.isPinCodeVerifying = false;
            return;
          }

          //
          // if something else just display a error message
          //
          this.messageService.add({ severity: "error", summary: "Unknown error" });
          this.isPinCodeVerifying = false;
        }
      })
    //TODO: next steps
  }
}
