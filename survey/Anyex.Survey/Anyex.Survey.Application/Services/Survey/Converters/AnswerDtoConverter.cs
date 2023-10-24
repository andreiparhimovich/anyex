using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Application.Services.Survey.Converters;

public class AnswerDtoConverter
{
    public BaseAnswer ToDomainModel(AnswerDto answerDto)
    {
        return answerDto.QuestionType switch
        {
            QuestionType.SingleOption => new SingleChoiceQuestionAnswer(answerDto.QuestionIndexNumber,
                new SelectedOption(answerDto.SelectedOption!.IndexNumber)),
            QuestionType.MultipleOption => new MultipleChoiceQuestionAnswer(answerDto.QuestionIndexNumber,
                new List<SelectedOption>
                (
                    answerDto.SelectedOptions.Select(x => new SelectedOption(x.IndexNumber)).ToList()
                )),
            QuestionType.Rating => new RatingAnswer(answerDto.QuestionIndexNumber, answerDto.Rating),
            QuestionType.FreeText => new FreeTextAnswer(answerDto.QuestionIndexNumber, answerDto.Text!),
            _ => throw new Exception($"Question Type {answerDto.QuestionType} is not allowed")
        };
    }

    public AnswerDto ToDtoModel(BaseAnswer answer)
    {
        var answerDto = new AnswerDto
        {
            QuestionIndexNumber = answer.QuestionIndexNumber
        };

        if (answer is SingleChoiceQuestionAnswer singleOptionAnswer)
        {
            answerDto.QuestionType = QuestionType.SingleOption;
            answerDto.SelectedOption = new SelectedOptionDto
            {
                IndexNumber = singleOptionAnswer.SelectedOption.IndexNumber
            };
        }

        if (answer is MultipleChoiceQuestionAnswer multipleOptionsAnswer)
        {
            answerDto.QuestionType = QuestionType.MultipleOption;
            answerDto.SelectedOptions = multipleOptionsAnswer.SelectedOptions.Select(x => new SelectedOptionDto
            {
                IndexNumber = x.IndexNumber
            }).ToList();
        }

        if (answer is RatingAnswer ratingAnswer)
        {
            answerDto.QuestionType = QuestionType.Rating;
            answerDto.Rating = ratingAnswer.Rating;
        }

        if (answer is FreeTextAnswer commentAnswer)
        {
            answerDto.QuestionType = QuestionType.FreeText;
            answerDto.Text = commentAnswer.Text;
        }

        return answerDto;
    }
}