import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators, ValidatorFn } from '@angular/forms';
import Question from 'src/app/common/Question';
import QuestionOption from 'src/app/common/QuestionOption';
import QuestionOptionViewModel from 'src/app/common/QuestionOptionViewModel';
import QuestionType from 'src/app/common/QuestionType';

@Component({
  selector: 'app-single-choice-question-editor',
  templateUrl: './single-choice-question-editor.component.html',
  styleUrls: ['./single-choice-question-editor.component.css']
})
export class SingleChoiceQuestionEditorComponent implements AfterViewInit {

  @Output() submit = new EventEmitter<Question>();

  questionOptions: QuestionOptionViewModel[] = [
    {
      text: '',
      indexNumber: 1,
      formFieldName: "option_1",
      isPossibleToDelete: false
    }
  ];

  singleChoiceQuestionFormGroup: FormGroup;

  get questionText() { return this.singleChoiceQuestionFormGroup.get('questionText') }

  constructor() {
    this.singleChoiceQuestionFormGroup = this.buildForm();
  }

  ngAfterViewInit(): void {
    // const questionTextInput = document.getElementById('single-choice-question-text-input-id');
    // questionTextInput?.focus();

    // const questionAddButton = document.getElementById('single-choice-question-add-button-id');
    // questionAddButton!.scrollIntoView({ behavior: 'smooth', block: 'end' });
  }

  formValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    const optionFields = this.questionOptions.map(option => {
      return control.get(option.formFieldName) as FormControl;
    });

    if (!optionFields.some(field => field.value !== '')) {
      return { atLeastOneOption: true }
    }

    return null;
  }

  buildForm() {

    const formFields: any = {
      questionText: new FormControl('', [Validators.required, Validators.maxLength(250)])
    };

    this.questionOptions.forEach(option => {
      formFields[option.formFieldName] = new FormControl(option.text || '', Validators.maxLength(250))
    });

    return new FormGroup(formFields, { validators: this.formValidator });
  }

  resetFormAndOptions() {
    this.singleChoiceQuestionFormGroup?.reset();

    this.questionOptions = [
      {
        text: '',
        indexNumber: 1,
        formFieldName: "option_1",
        isPossibleToDelete: false
      }
    ];

    this.singleChoiceQuestionFormGroup = this.buildForm();
  }

  onSubmit(event: SubmitEvent) {

    event.stopPropagation();

    const notEmptyOptions = this.questionOptions
      .filter(option => this.singleChoiceQuestionFormGroup.controls[option.formFieldName].value !== '');

    const options = notEmptyOptions.map((option, index) => <QuestionOption>{
      indexNumber: index + 1,
      text: this.singleChoiceQuestionFormGroup.controls[option.formFieldName].value,
    });

    const question: Question = {
      indexNumber: 1,
      questionType: QuestionType.SingleChoice,
      text: this.singleChoiceQuestionFormGroup?.value.questionText ?? '',
      maxRating: 0,
      options: options
    }

    this.submit.emit(question);

    this.resetFormAndOptions();
  }

  resetClickHandler() {
    this.resetFormAndOptions();
  }

  onOptionTextChange(text: string, indexNumber: number) {
    this.addBlankOptionIfLastUpdated(text, indexNumber);
    this.makeSureOptionsArePossibleToDelete();
  }

  addBlankOptionIfLastUpdated(text: string, indexNumber: number) {

    const maxIndex = Math.max.apply(null, this.questionOptions.map(option => option.indexNumber));

    if (text !== '' && maxIndex === indexNumber) {

      const newOption = {
        text: '',
        indexNumber: indexNumber + 1,
        isPossibleToDelete: false,
        formFieldName: `option_${indexNumber + 1}`
      };

      this.questionOptions.push(newOption);

      this.singleChoiceQuestionFormGroup.addControl(newOption.formFieldName, new FormControl(''));
    }
  }

  removeOption(indexNumber: number) {
    const indexToRemove = this.questionOptions.findIndex(option => option.indexNumber === indexNumber);

    if (indexNumber !== -1) {
      this.questionOptions.splice(indexToRemove, 1);
      this.makeSureOptionsArePossibleToDelete();

      this.singleChoiceQuestionFormGroup.removeControl(`option_${indexNumber}`);
    }
  }

  makeSureOptionsArePossibleToDelete() {

    this.questionOptions.forEach(option => option.isPossibleToDelete = true);

    if (this.questionOptions.length === 1) {
      this.questionOptions[0].isPossibleToDelete = false;
    }

    const lastQuestionOption = this.questionOptions[this.questionOptions.length - 1];

    if (lastQuestionOption.text === '') {
      lastQuestionOption.isPossibleToDelete = false;
    }
  }
}

