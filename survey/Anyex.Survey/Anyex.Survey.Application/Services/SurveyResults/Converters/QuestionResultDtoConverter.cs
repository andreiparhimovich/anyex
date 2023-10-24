using Anyex.Survey.Application.Services.SurveyResults.Dtos;
using Anyex.Survey.Domain.SurveyResults;

namespace Anyex.Survey.Application.Services.SurveyResults.Converters;

public class QuestionResultDtoConverter
{
    public QuestionResultDto ToDtoModel(BaseQuestionResult questionResult)
    {
        var questionResultDto = new QuestionResultDto
        {
            QuestionType = questionResult.QuestionType,
            IndexNumber = questionResult.QuestionIndexNumber,
            QuestionText = questionResult.Text,
            NumberOfAnswers = questionResult.NumberOfAnswers
        };

        if (questionResult is ChoiceQuestionResult choiceQuestionResult)
        {
            questionResultDto.OptionsResults = choiceQuestionResult.OptionsResults.Select(x =>
                new QuestionOptionResultDto
                {
                    NumberOfAnswers = x.NumberOfAnswers,
                    OptionText = x.OptionText,
                    Percentage = x.Percentage,
                    IndexNumber = x.OptionIndexNumber
                }).ToList();
        }

        if (questionResult is RatingQuestionResult ratingQuestionResult)
        {
            questionResultDto.AverageRating = ratingQuestionResult.AverageRating;
            questionResultDto.MaxRating = ratingQuestionResult.MaxRating;
        }

        if (questionResult is FreeTextQuestionResult commentQuestionResult)
        {
            questionResultDto.AnswerTexts = commentQuestionResult.AnswerTexts;
        }

        return questionResultDto;
    }
}