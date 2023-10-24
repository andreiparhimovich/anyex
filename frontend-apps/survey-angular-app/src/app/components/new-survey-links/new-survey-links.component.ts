import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Clipboard } from '@angular/cdk/clipboard';
import { GetSurveySteps } from 'src/app/common/Utils';
import { SurveyBuilderService, SurveyBuilderSteps } from 'src/app/services/survey-builder.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-new-survey-links',
  templateUrl: './new-survey-links.component.html',
  styleUrls: ['./new-survey-links.component.css']
})
export class NewSurveyLinksComponent {

  surveySteps: any[] = GetSurveySteps();

  surveyId: string = '';
  surveyUrl: string = '';
  surveyResultsUrl: string = '';

  constructor(
    private router: Router,
    private clipboard: Clipboard,
    private surveyBuilder: SurveyBuilderService,
    private messageService: MessageService) {

    //
    // if the survey builder is NOT on 'Links' step, then reset the survey and redirect to '/home'
    //
    const surveyBuilderCurrentStep = this.surveyBuilder.getCurrentStep();

    if (surveyBuilderCurrentStep !== SurveyBuilderSteps.Links) {
      this.surveyBuilder.reset();
      this.router.navigate(['/']);
    }

    //
    // proceed with survey instance from the builder. Cannot be null in all the cases
    //
    const survey = this.surveyBuilder.getInstance();

    if (survey) {
      this.surveyId = survey.id;
      this.surveyUrl = survey.surveyUrl;
      this.surveyResultsUrl = survey.surveyResultsUrl;

      //
      // Once we initialized the url properties me may reset the survey
      //
      this.surveyBuilder.reset();

    } else {
      throw new Error("Survey cannot be null");
    }
  }

  copySurveyLinkToClipboard() {
    this.clipboard.copy(this.surveyUrl);
    this.messageService.add({ severity: "success", summary: "Copied to clipboard" });
  }

  copySurveyResultLinkToClipboard() {
    this.clipboard.copy(this.surveyResultsUrl);
    this.messageService.add({ severity: "success", summary: "Copied to clipboard" });
  }

  createNewSurvey() {
    //
    // set survey builder step to 'Questions' and go to '/new/questions' page
    //
    this.surveyBuilder.setCurrentStep(SurveyBuilderSteps.Questions);
    this.router.navigate(['/new/questions']);
  }

  goToSurvey() {
    const url = this.router.serializeUrl(
      this.router.createUrlTree(['/survey', this.surveyId])
    );

    window.open(url, '_blank');
  }
}
