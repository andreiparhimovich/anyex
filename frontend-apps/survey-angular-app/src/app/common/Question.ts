import QuestionOption from "./QuestionOption";
import QuestionType from "./QuestionType";

interface Question {
    indexNumber: number,
    text: string | undefined,
    questionType: QuestionType,
    maxRating: number | undefined,
    options: QuestionOption[]
}

export default Question;