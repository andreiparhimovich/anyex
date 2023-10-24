using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Domain.Questions;
using FluentValidation;

namespace Anyex.Survey.Application.Services.Survey.Validators;

public class AnswerDtoValidator : AbstractValidator<AnswerDto> 
{
    public AnswerDtoValidator()
    {
        RuleFor(x => x.QuestionIndexNumber)
            .GreaterThan(0)
            .WithMessage($"Question Index Number must be greater than 0");

        When(x => x.QuestionType == QuestionType.MultipleOption, () =>
        {
            RuleFor(x => x.SelectedOptions)
                .NotEmpty()
                .WithMessage(x => $"At least one option should be selected when answer type is {x.QuestionType}");
        });

        When(x => x.QuestionType == QuestionType.SingleOption, () =>
        {
            RuleFor(x => x.SelectedOption)
                .NotNull()
                .WithMessage(x => $"Option can't be null when answer type is {x.QuestionType}");

            When(x => x.SelectedOption is not null, () =>
            {
                RuleFor(x => x.SelectedOption!.IndexNumber)
                    .GreaterThan(0)
                    .WithMessage("Selected Question Index must be greater than 0");
            });
        });

        When(x => x.QuestionType == QuestionType.FreeText, () =>
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text should not be empty");
        });

        When(x => x.QuestionType == QuestionType.Rating, () =>
        {
            RuleFor(x => x.Rating)
                .GreaterThan(0)
                .WithMessage("Rating must be greater than 0");
        });
    }
}