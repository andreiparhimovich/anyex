import { Component, EventEmitter, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import Question from 'src/app/common/Question';
import QuestionOption from 'src/app/common/QuestionOption';
import QuestionOptionViewModel from 'src/app/common/QuestionOptionViewModel';
import QuestionType from 'src/app/common/QuestionType';

@Component({
  selector: 'app-multiple-choice-question-editor',
  templateUrl: './multiple-choice-question-editor.component.html',
  styleUrls: ['./multiple-choice-question-editor.component.css']
})
export class MultipleChoiceQuestionEditorComponent {
  // update add button width

  @Output() submit = new EventEmitter<Question>();

  questionOptions: QuestionOptionViewModel[] = [
    {
      text: '',
      indexNumber: 1,
      formFieldName: "option_1",
      isPossibleToDelete: false
    }
  ];

  multipleChoiceQuestionFormGroup: FormGroup;

  get questionText() { return this.multipleChoiceQuestionFormGroup.get('questionText') }

  constructor() {
    this.multipleChoiceQuestionFormGroup = this.buildForm();
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
    this.multipleChoiceQuestionFormGroup?.reset();

    this.questionOptions = [
      {
        text: '',
        indexNumber: 1,
        formFieldName: "option_1",
        isPossibleToDelete: false
      }
    ];

    this.multipleChoiceQuestionFormGroup = this.buildForm();
  }

  onSubmit(event: SubmitEvent) {

    event.stopPropagation();

    const notEmptyOptions = this.questionOptions
      .filter(option => this.multipleChoiceQuestionFormGroup.controls[option.formFieldName].value !== '');

    const options = notEmptyOptions.map((option, index) => <QuestionOption>{
      indexNumber: index + 1,
      text: this.multipleChoiceQuestionFormGroup.controls[option.formFieldName].value,
    });

    const question: Question = {
      indexNumber: 1,
      questionType: QuestionType.MultipleChoice,
      text: this.multipleChoiceQuestionFormGroup?.value.questionText ?? '',
      maxRating: 0,
      options: options
    }

    console.log(question);

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

      this.multipleChoiceQuestionFormGroup.addControl(newOption.formFieldName, new FormControl(''));
    }
  }

  removeOption(indexNumber: number) {
    const indexToRemove = this.questionOptions.findIndex(option => option.indexNumber === indexNumber);

    if (indexNumber !== -1) {
      this.questionOptions.splice(indexToRemove, 1);
      this.makeSureOptionsArePossibleToDelete();

      this.multipleChoiceQuestionFormGroup.removeControl(`option_${indexNumber}`);
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
