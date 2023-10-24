import QuestionType from "./QuestionType";
import { SurveyResultsAccessType } from "./Survey";

export function GetQuestionTypeString(questionType: string | QuestionType): string {
    switch (questionType) {
        case QuestionType.SingleChoice:
            return "Single Choice"
        case QuestionType.MultipleChoice:
            return "Multiple Choice"
        case QuestionType.Rating:
            return "Rating"
        case QuestionType.FreeText:
            return "Free Text"
        default:
            return "";
    }
}

export function GetSurveySteps() {
    return [
        { label: "Add Questions" },
        { label: "Set Up Settings" },
        { label: "Get Links" },
    ];
}

export function getResultsAccesTypeString(resultsAccessType: SurveyResultsAccessType) {
    switch (resultsAccessType) {
        case SurveyResultsAccessType.Public:
            return "Public";
        case SurveyResultsAccessType.ByPinCode:
            return "By PIN code";
        default:
            return "";
    }
}