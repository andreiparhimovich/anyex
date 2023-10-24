using System.Security.Cryptography.X509Certificates;
using Anyex.Survey.Application.Services.Survey.Dtos.CreateSurvey;
using Anyex.Survey.Domain;
using FluentValidation;

namespace Anyex.Survey.Application.Services.Survey.Validators;

public class CreateSurveyDtoValidator : AbstractValidator<CreateSurveyDto>
{
    public CreateSurveyDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(250)
            .WithMessage("Survey Title length must be less or equal to 250");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Survey Description length must be less or equal to 1000");

        When(x => x.UseExpirationDate, () =>
        {
            RuleFor(x => x.ExpirationDateUtc)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Expiration date must be greater than current date and time. Actual value");
        });

        When(x => x.ResultsAccessType is SurveyResultsAccessType.ByPinCode, () =>
        {
            RuleFor(x => x.PinCode)
                .NotEmpty()
                .WithMessage("Pin Code cannot be empty");

            RuleFor(x => x.PinCode)
                .MaximumLength(8)
                .WithMessage("Pin Code length must be less or equal to 8");
        });

        RuleFor(x => x.Questions)
            .Must(x => x.Count <= 10)
            .WithMessage("Maximum number of questions in a survey is 10");

        RuleFor(x => x.Questions)
            .Must(x => x.Count > 0)
            .WithMessage("Surveys must contain at least 1 question");

        RuleForEach(x => x.Questions).SetValidator(new QuestionDtoValidator());
    }
}
