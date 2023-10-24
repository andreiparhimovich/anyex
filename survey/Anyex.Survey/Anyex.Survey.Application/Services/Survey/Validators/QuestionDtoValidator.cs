using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Domain.Questions;
using FluentValidation;

namespace Anyex.Survey.Application.Services.Survey.Validators;

public class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(x => x.IndexNumber)
            .GreaterThan(0)
            .WithMessage("Question Index Number must be greater than 0");

        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Question Text cannot be empty")
            .MaximumLength(250)
            .WithMessage("Question Text length must be less or equal to 250");

        When(x => x.QuestionType is QuestionType.SingleOption or QuestionType.MultipleOption, () =>
        {
            RuleFor(x => x.Options)
                .NotNull().NotEmpty()
                .WithMessage("Question Options must contain at least 1 option");

            RuleForEach(x => x.Options).SetValidator(new QuestionOptionDtoValidator());
        });

        When(x => x.QuestionType is QuestionType.Rating, () =>
        {
            RuleFor(x => x.MaxRating)
                .GreaterThan(0)
                .WithMessage("Question Rating must be greater than 0");
        });
    }

    class QuestionOptionDtoValidator : AbstractValidator<QuestionOptionDto>
    {
        public QuestionOptionDtoValidator()
        {
            RuleFor(x => x.IndexNumber)
                .GreaterThan(0)
                .WithMessage("Question Option Index must be grater than 0");

            RuleFor(x => x.Text)
                .NotNull().NotEmpty()
                .WithMessage("Question Option Text should not be empty")
                .MaximumLength(250)
                .WithMessage("Question Option Text length must be less or equal to 250");
        }
    }
}