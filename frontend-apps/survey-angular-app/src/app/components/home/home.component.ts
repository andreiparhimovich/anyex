import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SurveyBuilderService, SurveyBuilderSteps } from '../../services/survey-builder.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private router: Router, private surveyBuilder: SurveyBuilderService) { }

  createNewSurvey() {
    this.surveyBuilder.reset();
    this.surveyBuilder.setCurrentStep(SurveyBuilderSteps.Questions);

    this.router.navigate(['new/questions']);
  }
}
