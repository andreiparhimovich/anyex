import Question from "./Question";

export default interface Survey {
    id: string;
    title: string;
    questions: Question[];
    surveyUrl: string;
    surveyResultsUrl: string;
    description: string;
    useExpirationDate: boolean;
    expirationDateUtc: Date;
    resultsAccessType: SurveyResultsAccessType;
    pinCode: string;
}

export enum SurveyResultsAccessType {
    Public = 0,
    ByPinCode = 1,
}