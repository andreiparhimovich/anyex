import QuestionType from "./QuestionType";

export interface AnswerViewModel {
    QuestionIndexNumber: number;
    QuestionType: QuestionType;
    SelectedOption: SelectedOption | undefined;
    SelectedOptions: SelectedOption[] | undefined;
    Text: string | undefined;
    Rating: number | undefined;
    IsReadyToAnswer: boolean;
}

export interface SelectedOption {
    IndexNumber: number;
}

