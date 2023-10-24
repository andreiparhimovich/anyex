import { Component } from '@angular/core';
import { SurveyService } from '../../services/survey.service';
import { Router } from '@angular/router';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { SurveyBuilderService, SurveyBuilderSteps } from '../../services/survey-builder.service';
import { GetSurveySteps, getResultsAccesTypeString } from '../../common/Utils';
import { SurveyResultsAccessType } from 'src/app/common/Survey';
import { CreateSurveyResultsModel } from 'src/app/common/CreateSurveyResultsModel';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-new-survey-settings',
  templateUrl: './new-survey-settings.component.html',
  styleUrls: ['./new-survey-settings.component.css']
})
export class NewSurveySettingsComponent {

  newSurveySettingsFormGroup: FormGroup = new FormGroup({});

  surveySteps: any[] = GetSurveySteps();

  isCreateRequestSending: boolean = false;

  isDesktop: boolean = false;

  expirationMinDate: Date = new Date();

  get isSurveyResultsAccessPinCode() {
    return this.newSurveySettingsFormGroup.get('resultsAccessType')?.value?.value === SurveyResultsAccessType.ByPinCode
  };

  get pinCode() { return this.newSurveySettingsFormGroup.get('pinCode') };

  get description() { return this.newSurveySettingsFormGroup.get('description') };

  get useExpirationDate() { return this.newSurveySettingsFormGroup.get('useExpirationDate')?.value };

  get surveyExpirationDate() { return this.newSurveySettingsFormGroup.get('surveyExpirationDate') };

  get usePinCodeProtection() { return this.newSurveySettingsFormGroup.get('usePinCodeProtection')?.value };


  pinCodeFiledValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    const pinCodeValue = control.value;

    if (this.newSurveySettingsFormGroup) {
      const usePinCodeProtection = this.newSurveySettingsFormGroup.get('usePinCodeProtection')?.value;

      if (usePinCodeProtection) {
        if (!pinCodeValue) {
          return { required: true };
        }

        if (pinCodeValue.length > 8) {
          return { maxLength: true };
        }
      }
    }

    return null;
  }

  expirationDateValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    const expirationDateValue = control.value;

    const currentDateTime = new Date();

    if (expirationDateValue <= currentDateTime) {
      return { expirationDateLessThanCurrent: true }
    }

    return null;
  }

  constructor(
    private router: Router,
    private breakpointObserver: BreakpointObserver,
    private surveyBuilder: SurveyBuilderService,
    private surveyService: SurveyService,
    private messageService: MessageService) {

    this.subscribeScreenSizeChanges();

    //
    // if survey builder is NOT on 'Settings' step then reset the survey and redirect to '/'
    //
    const surveyBuilderCurrentStep = this.surveyBuilder.getCurrentStep();

    if (surveyBuilderCurrentStep !== SurveyBuilderSteps.Settings) {
      this.surveyBuilder.reset();
      this.router.navigate(['/']);
    }

    //
    // proceed with survey instance from the builder. Cannot be null in all the cases
    //
    const survey = surveyBuilder.getInstance();

    if (survey) {

      this.expirationMinDate = new Date();

      //
      // herer I get current survey fields values and initialize the form
      ///
      const description = survey.description;
      const useExpirationDate = survey.useExpirationDate;
      const surveyExpirationDate = survey.expirationDateUtc;
      const resultsAccessType = survey.resultsAccessType;
      const pinCode = survey.pinCode;

      this.newSurveySettingsFormGroup = new FormGroup({
        description: new FormControl(description, [Validators.maxLength(1000)]),
        useExpirationDate: new FormControl(useExpirationDate),
        surveyExpirationDate: new FormControl(surveyExpirationDate, { validators: this.expirationDateValidator }),
        usePinCodeProtection: new FormControl(resultsAccessType == SurveyResultsAccessType.ByPinCode),
        pinCode: new FormControl(pinCode, { validators: this.pinCodeFiledValidator }),
      });

      //
      // here I subcsribe on value changes for usePinCodeProtection field
      //  in order to update validation for PIN Code field every time the use pin code protection field is updated
      //
      this.newSurveySettingsFormGroup.get('usePinCodeProtection')?.valueChanges.subscribe(() => {
        this.newSurveySettingsFormGroup.get('pinCode')?.updateValueAndValidity();
      });

    } else {
      throw new Error("Survey cannot be null");
    }
  }

  subscribeScreenSizeChanges() {
    this.breakpointObserver.observe([Breakpoints.Web, Breakpoints.WebLandscape]).subscribe(result => {
      this.isDesktop = result.matches;
    });
  }

  onCreateSurvey(event: SubmitEvent) {

    event.stopPropagation();

    //
    // enforce validation
    //
    Object.keys(this.newSurveySettingsFormGroup.controls).forEach((key: string) => {
      const control = this.newSurveySettingsFormGroup.get(key);
      control!.markAsTouched();
      control!.updateValueAndValidity();
    });

    if (this.newSurveySettingsFormGroup.valid) {
      this.isCreateRequestSending = true;

      const description = this.newSurveySettingsFormGroup.get('description')?.value;
      const useExpirationDate = this.newSurveySettingsFormGroup.get('useExpirationDate')?.value;
      const expirationDate = this.newSurveySettingsFormGroup.get('surveyExpirationDate')?.value;
      const usePinCodeProtection = this.newSurveySettingsFormGroup.get('usePinCodeProtection')?.value;
      const pinCode = this.newSurveySettingsFormGroup.get('pinCode')?.value;

      const resultsAccessType = usePinCodeProtection ? SurveyResultsAccessType.ByPinCode : SurveyResultsAccessType.Public;

      this.surveyBuilder.addSettings(description, useExpirationDate, expirationDate, resultsAccessType, pinCode);

      //
      // get current survey instance and try to call 'create' endpoint
      //
      const survey = this.surveyBuilder.getInstance();

      if (survey) {
        this.surveyService.createSurvey(survey)
          .subscribe({
            next: (response) => {

              this.isCreateRequestSending = false;

              const createSurveyResultsModel = response as CreateSurveyResultsModel;

              this.surveyBuilder.setId(createSurveyResultsModel.surveyId);
              this.surveyBuilder.addLinks(createSurveyResultsModel.surveyUrl, createSurveyResultsModel.surveyResultsUrl);

              //
              // the current survey builder step must be only 'Settings'. Set 'Links' and proceed
              //
              this.surveyBuilder.setCurrentStep(SurveyBuilderSteps.Links);

              this.router.navigate(['/new/links']);
            },
            error: () => {
              this.messageService.add({ severity: "error", summary: "Unknown error while creating a survey." });

              this.isCreateRequestSending = false;
            }
          });
      } else {
        throw new Error("Survey cannot be null");
      }
    }
  }

  goBack() {
    const description = this.newSurveySettingsFormGroup.get('description')?.value;
    const useExpirationDate = this.newSurveySettingsFormGroup.get('useExpirationDate')?.value;
    const expirationDate = this.newSurveySettingsFormGroup.get('surveyExpirationDate')?.value;
    const usePinCodeProtection = this.newSurveySettingsFormGroup.get('usePinCodeProtection')?.value;
    const pinCode = this.newSurveySettingsFormGroup.get('pinCode')?.value;

    const resultsAccessType = usePinCodeProtection ? SurveyResultsAccessType.ByPinCode : SurveyResultsAccessType.Public;

    this.surveyBuilder.addSettings(description, useExpirationDate, expirationDate, resultsAccessType, pinCode);

    this.router.navigate(['/new/questions']);
  }
}
