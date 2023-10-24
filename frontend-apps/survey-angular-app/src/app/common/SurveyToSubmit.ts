import Question from "./Question";

export default interface SurveyToSubmit {
    surveyId: string;
    title: string;
    isExpired: boolean;
    description: string;
    questions: Question[];
}