import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from '../../services/survey.service';
import { MessageService } from 'primeng/api';
import SurveyToSubmit from 'src/app/common/SurveyToSubmit';
import { AnswerViewModel } from 'src/app/common/AnswerViewModel';

@Component({
  selector: 'app-submit-survey',
  templateUrl: './submit-survey.component.html',
  styleUrls: ['./submit-survey.component.css']
})
export class SubmitSurveyComponent {
  isSurveyLoading: boolean = false;
  isSurveySubmitting: boolean = false;
  isSurveySubmitted: boolean = false;

  survey!: SurveyToSubmit;

  get surveyIsExpired() { return this.survey.isExpired }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private surveyService: SurveyService,
    private messageService: MessageService) {

    //
    // mark that a survey is loading from the server to prevent display undefined elements
    //
    this.isSurveyLoading = true;

    const surveyId = this.route.snapshot.paramMap.get('id')!;

    this.surveyService.getSurveyToSubmit(surveyId)
      .subscribe({
        next: (response) => {
          //
          // set survey model
          //
          this.survey = response as SurveyToSubmit;

          //
          //mark that we stopped loading from the server
          this.isSurveyLoading = false;
        },
        error: (error) => {
          if (error.status == 404) {
            this.router.navigate(['/notfound']);
          } else {
            this.messageService.add({ severity: "error", summary: "Unknown error" });
          }

          this.isSurveyLoading = false;
        }
      })
  }

  onAnswersSubmit(answers: AnswerViewModel[]) {

    this.isSurveySubmitting = true;

    //
    // filter only answers that is not null. They may be null if a user skip a question
    //
    const answersToSubmit = answers.filter(value => value !== null);

    this.surveyService.submitAnswers(this.survey.surveyId, answersToSubmit)
      .subscribe({
        next: () => {
          this.isSurveySubmitting = false;
          this.isSurveySubmitted = true;
        },
        error: () => {
          this.isSurveySubmitting = false;
          this.messageService.add({ severity: "error", summary: "Unknown error while submitting the survey." });
        }
      })
  }

  goToResults() {
    const url = this.router.serializeUrl(
      this.router.createUrlTree(['/survey', this.survey.surveyId, 'results'])
    );

    window.open(url, '_blank');
  }

  goToHome() {
    this.router.navigate(['home']);
  }
}
