import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AnswerViewModel, SelectedOption } from 'src/app/common/AnswerViewModel';
import Question from 'src/app/common/Question';
import { GetQuestionTypeString } from 'src/app/common/Utils';

@Component({
  selector: 'app-multiple-choice-answer',
  templateUrl: './multiple-choice-answer.component.html',
  styleUrls: ['./multiple-choice-answer.component.css']
})
export class MultipleChoiceAnswerComponent implements OnInit {
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

  buildForm() {
    const formFields: any = {};

    this.question.options.forEach(questionOption => {
      formFields[questionOption.indexNumber] = new FormControl<boolean>(false);
    });

    this.answer?.SelectedOptions?.forEach(selectedOption => {
      formFields[selectedOption.IndexNumber].setValue(true);
    })

    return new FormGroup(formFields);
  }

  onFormValueChange(controlKey: string, value: any) {
    //
    //Create answer object to rise a change event
    //
    var selectedOptions: SelectedOption[] = [];

    Object.keys(this.questionOptionsFormGroup.controls).forEach(key => {
      if (this.questionOptionsFormGroup.controls[key].value === true) {
        selectedOptions?.push({
          IndexNumber: Number(key)
        });
      }
    });

    const answer: AnswerViewModel = {
      QuestionIndexNumber: this.question.indexNumber,
      QuestionType: this.question.questionType,
      SelectedOption: undefined,
      SelectedOptions: selectedOptions,
      Text: undefined,
      Rating: undefined,
      IsReadyToAnswer: selectedOptions.length > 0
    }

    this.answerChange.emit(answer);
  }
}
