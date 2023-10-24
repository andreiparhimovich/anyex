import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NewSurveyQuestionsComponent } from './components/new-survey-questions/new-survey-questions.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NewSurveySettingsComponent } from './components/new-survey-settings/new-survey-settings.component';
import { NewSurveyLinksComponent } from './components/new-survey-links/new-survey-links.component';
import { SubmitSurveyComponent } from './components/submit-survey/submit-survey.component';
import { TestComponent } from './components/test/test.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { TermsComponent } from './components/terms/terms.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'test', component: TestComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'privacy', component: PrivacyComponent },
  { path: 'terms', component: TermsComponent },
  { path: 'new/questions', component: NewSurveyQuestionsComponent },
  { path: 'new/settings', component: NewSurveySettingsComponent },
  { path: 'new/links', component: NewSurveyLinksComponent },
  { path: 'survey/:id', component: SubmitSurveyComponent },
  { path: 'survey/:id/results', component: SurveyResultsComponent },
  { path: 'notfound', pathMatch: 'full', component: PageNotFoundComponent },
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', pathMatch: 'full', component: PageNotFoundComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: "enabled" })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
