import { Component, Input, ElementRef, ViewChild, AfterViewInit, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AnswerViewModel } from 'src/app/common/AnswerViewModel';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-single-choice-answer',
  templateUrl: './single-choice-answer.component.html',
  styleUrls: ['./single-choice-answer.component.css']
})
export class SingleChoiceAnswerComponent implements OnInit {
  @Input() question!: Question;
  @Input() answer: AnswerViewModel | undefined;
  @Output() answerChange = new EventEmitter<AnswerViewModel>();

  @ViewChild('toggleButtons') toggleButtons!: ElementRef;

  questionOptionsFormGroup!: FormGroup;

  get questionType() { return GetQuestionTypeString(this.question.questionType) }

  ngOnInit() {
    this.questionOptionsFormGroup = this.buildForm();

    //
    // Subscribe to handle if the certain field/toggle button value changed
    //
    Object.keys(this.questionOptionsFormGroup.controls).forEach(key => {
      this.questionOptionsFormGroup.controls[key].valueChanges.subscribe(value => {
        this.onFormValueChange(key, value);
      });
    });
  }

  ngAfterViewInit() {
    //
    // Adjust the size of all the toggle buttons. Set the biggest for all.
    //
    const toggleButtons = this.toggleButtons.nativeElement.querySelectorAll('.p-togglebutton');
    let maxWidth = 0;

    toggleButtons.forEach((button: HTMLElement) => {
      const buttonWidth = button.offsetWidth;
      if (buttonWidth > maxWidth) {
        maxWidth = buttonWidth;
      }
    });

    // Set the maximum width for all toggle buttons
    toggleButtons.forEach((button: HTMLElement) => {
      button.style.width = `${maxWidth + 1}px`;
    });
  }

  buildForm() {
    const formFields: any = {};

    this.question.options.forEach(option => {
      const value = this.answer?.SelectedOption?.IndexNumber == option.indexNumber;
      formFields[option.indexNumber] = new FormControl<boolean>(value);
    });

    return new FormGroup(formFields);
  }

  onFormValueChange(controlKey: string, value: any) {
    //
    // Check if we need to unpress any button
    //
    if (value === true) {
      const optionIndex = controlKey;

      Object.keys(this.questionOptionsFormGroup.controls).forEach(key => {
        if (key !== optionIndex) {
          this.questionOptionsFormGroup.controls[key].setValue(false);
        }
      });
    }

    //
    //Create answer object to rise a change event
    //
    var selectedOption: string | undefined;

    Object.keys(this.questionOptionsFormGroup.controls).forEach(key => {
      if (this.questionOptionsFormGroup.controls[key].value === true) {
        selectedOption = key;
      }
    });

    const answer: AnswerViewModel = {
      QuestionIndexNumber: this.question.indexNumber,
      QuestionType: this.question.questionType,
      SelectedOption: selectedOption ? {
        IndexNumber: Number(selectedOption)
      } : undefined,
      SelectedOptions: undefined,
      Text: undefined,
      Rating: undefined,
      IsReadyToAnswer: selectedOption ? true : false
    }

    this.answerChange.emit(answer);
  }
}
