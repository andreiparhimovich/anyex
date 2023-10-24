using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Application.Services.Survey.Converters;

public class QuestionDtoConverter
{
    public BaseQuestion ToDomainModel(QuestionDto question)
    {
        return question.QuestionType switch
        {
            QuestionType.SingleOption => new SingleChoiceQuestion(question.IndexNumber, question.Text!,
                new List<QuestionOption>(question.Options.Select(x => new QuestionOption(x.IndexNumber, x.Text!)))),
            QuestionType.MultipleOption => new MultipleChoiceQuestion(question.IndexNumber, question.Text!,
                new List<QuestionOption>(question.Options.Select(x => new QuestionOption(x.IndexNumber, x.Text!)))),
            QuestionType.Rating => new RatingQuestion(question.IndexNumber, question.Text!, question.MaxRating),
            QuestionType.FreeText => new FreeTextQuestion(question.IndexNumber, question.Text!),
            _ => throw new Exception($"Question Type {question.QuestionType} is not allowed")
        };
    }

    public QuestionDto ToDtoModel(BaseQuestion question)
    {
        var questionDto = new QuestionDto
        {
            IndexNumber = question.IndexNumber,
            Text = question.Text,
            QuestionType = question.QuestionType
        };

        if (question is SingleChoiceQuestion singleOptionsQuestion)
        {
            questionDto.Options = singleOptionsQuestion.Options.Select(x => new QuestionOptionDto
            {
                IndexNumber = x.IndexNumber,
                Text = x.Text
            }).ToList();
        }

        if (question is MultipleChoiceQuestion multipleOptionsQuestion)
        {
            questionDto.Options = multipleOptionsQuestion.Options.Select(x => new QuestionOptionDto
            {
                IndexNumber = x.IndexNumber,
                Text = x.Text
            }).ToList();
        }

        if (question is RatingQuestion ratingQuestion)
        {
            questionDto.MaxRating = ratingQuestion.MaxRating;
        }

        return questionDto;
    }
}