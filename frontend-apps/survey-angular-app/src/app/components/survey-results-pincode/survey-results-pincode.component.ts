import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-survey-results-pincode',
  templateUrl: './survey-results-pincode.component.html',
  styleUrls: ['./survey-results-pincode.component.css']
})
export class SurveyResultsPincodeComponent {

  @Input() isLoading = false;
  @Input() isPinCodeInvalid = true;

  @Output() verifyPinCodeClick = new EventEmitter<string>();

  surveyPinCodeFromGroup: FormGroup;

  get pinCode() { return this.surveyPinCodeFromGroup.get('pinCode') }

  constructor() {
    this.surveyPinCodeFromGroup = new FormGroup({
      pinCode: new FormControl('', [Validators.required])
    })
  }

  onSubmit() {
    this.verifyPinCodeClick.emit(this.pinCode?.value);
  }
}
