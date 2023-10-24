import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { RatingModule } from 'primeng/rating';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TimelineModule } from 'primeng/timeline';
import { StepsModule } from 'primeng/steps';
import { CalendarModule } from 'primeng/calendar';
import { InputSwitchModule } from 'primeng/inputswitch';
import { TooltipModule } from 'primeng/tooltip';
import { ToastModule } from 'primeng/toast';
import { BadgeModule } from 'primeng/badge';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ProgressBarModule } from 'primeng/progressbar';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { TableModule } from 'primeng/table';
import { ImageModule } from 'primeng/image';
import { CardModule } from 'primeng/card';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NewSurveyQuestionsComponent } from './components/new-survey-questions/new-survey-questions.component';
import { HeaderComponent } from './components/header/header.component';
import { QuestionEditorContainerComponent } from './components/question-editor-container/question-editor-container.component';
import { FreeTextQuestionEditorComponent } from './components/free-text-question-editor/free-text-question-editor.component';
import { RatingQuestionEditorComponent } from './components/rating-question-editor/rating-question-editor.component';
import { SingleChoiceQuestionEditorComponent } from './components/single-choice-question-editor/single-choice-question-editor.component';
import { MultipleChoiceQuestionEditorComponent } from './components/multiple-choice-question-editor/multiple-choice-question-editor.component';
import { CommentQuestionPreviewComponent } from './components/free-text-question-preview/free-text-question-preview.component';
import { SingleChoiceQuestionPreviewComponent } from './components/single-choice-question-preview/single-choice-question-preview.component';
import { MultipleChoiceQuestionPreviewComponent } from './components/multiple-choice-question-preview/multiple-choice-question-preview.component';
import { RatingQuestionPreviewComponent } from './components/rating-question-preview/rating-question-preview.component';
import { NewSurveySettingsComponent } from './components/new-survey-settings/new-survey-settings.component';
import { NewSurveyLinksComponent } from './components/new-survey-links/new-survey-links.component';
import { SubmitSurveyComponent } from './components/submit-survey/submit-survey.component';
import { SingleChoiceAnswerComponent } from './components/single-choice-answer/single-choice-answer.component'
import { TestComponent } from './components/test/test.component';

import { MultipleChoiceAnswerComponent } from './components/multiple-choice-answer/multiple-choice-answer.component';
import { FreeTextAnswerComponent } from './components/free-text-answer/free-text-answer.component';
import { RatingAnswerComponent } from './components/rating-answer/rating-answer.component';
import { SurveyAnswersContainerComponent } from './components/survey-answers-container/survey-answers-container.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { SurveyResultsPincodeComponent } from './components/survey-results-pincode/survey-results-pincode.component';
import { ChoiceOptionResultChartComponent } from './components/choice-option-result-chart/choice-option-result-chart.component';
import { ChoiceQuestionResultComponent } from './components/choice-question-result/choice-question-result.component';
import { RatingQuestionResultComponent } from './components/rating-question-result/rating-question-result.component';
import { FreeTextQuestionResultComponent } from './components/free-text-question-result/free-text-question-result.component';
import { FooterComponent } from './components/footer/footer.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { TermsComponent } from './components/terms/terms.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PageNotFoundComponent,
    NewSurveyQuestionsComponent,
    HeaderComponent,
    QuestionEditorContainerComponent,
    FreeTextQuestionEditorComponent,
    RatingQuestionEditorComponent,
    SingleChoiceQuestionEditorComponent,
    MultipleChoiceQuestionEditorComponent,
    CommentQuestionPreviewComponent,
    SingleChoiceQuestionPreviewComponent,
    MultipleChoiceQuestionPreviewComponent,
    RatingQuestionPreviewComponent,
    NewSurveySettingsComponent,
    NewSurveyLinksComponent,
    SubmitSurveyComponent,
    TestComponent,
    SingleChoiceAnswerComponent,
    MultipleChoiceAnswerComponent,
    FreeTextAnswerComponent,
    RatingAnswerComponent,
    SurveyAnswersContainerComponent,
    SurveyResultsComponent,
    SurveyResultsPincodeComponent,
    ChoiceOptionResultChartComponent,
    ChoiceQuestionResultComponent,
    RatingQuestionResultComponent,
    FreeTextQuestionResultComponent,
    FooterComponent,
    AboutComponent,
    ContactComponent,
    PrivacyComponent,
    TermsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    CheckboxModule,
    MessagesModule,
    MessageModule,
    RatingModule,
    RadioButtonModule,
    TimelineModule,
    StepsModule,
    CalendarModule,
    InputSwitchModule,
    TooltipModule,
    ToastModule,
    BadgeModule,
    TagModule,
    ProgressSpinnerModule,
    ToggleButtonModule,
    ProgressBarModule,
    ScrollPanelModule,
    TableModule,
    ImageModule,
    CardModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
