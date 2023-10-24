import QuestionType from "./QuestionType";

export interface SurveyResults {
    surveyId: string;
    surveyTitle: string;
    numberOfSubmits: number;
    results: QuestionResult[];
}

export interface QuestionResult {
    questionType: QuestionType;
    indexNumber: number;
    questionText: string;
    numberOfAnswers: number;
    averageRating: number;
    maxRating: number;
    optionsResults: QuestionOptionResult[];
    answerTexts: string[];
}

export interface QuestionOptionResult {
    indexNumber: number;
    optionText: string;
    percentage: number;
    numberOfAnswers: number;
}