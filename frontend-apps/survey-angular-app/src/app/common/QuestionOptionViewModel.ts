import QuestionOption from 'src/app/common/QuestionOption';

interface QuestionOptionViewModel extends QuestionOption {
    formFieldName: string;
    isPossibleToDelete: boolean;
}

export default QuestionOptionViewModel;