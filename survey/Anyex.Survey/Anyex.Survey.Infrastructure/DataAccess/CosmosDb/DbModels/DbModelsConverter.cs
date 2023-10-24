using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Domain;
using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Infrastructure.DataAccess.CosmosDb.DbModels
{
    public class DbModelsConverter
    {
        public static SurveyDbModel ToDbModel(Domain.Survey survey)
        {
            var questionDbModels = survey.Questions.Select(ToDbModel).ToList();
            var submits = survey.SurveySubmits.Select(ToDbModel).ToList();

            return new SurveyDbModel(
                survey.Id,
                survey.Title,
                survey.Description,
                survey.ExpirationDateUtc,
                survey.ResultAccess.AccessType,
                survey.ResultAccess.PinCode,
                questionDbModels,
                submits);
        }

        private static QuestionDbModel ToDbModel(BaseQuestion question)
        {
            List<QuestionOptionDbModel> options = new List<QuestionOptionDbModel>();

            if (question is SingleChoiceQuestion singleOptionsQuestion)
            {
                options = singleOptionsQuestion.Options
                    .Select(option => new QuestionOptionDbModel(option.IndexNumber, option.Text))
                    .ToList();
            }

            if (question is MultipleChoiceQuestion multipleOptionsQuestion)
            {
                options = multipleOptionsQuestion.Options
                    .Select(option => new QuestionOptionDbModel(option.IndexNumber, option.Text))
                    .ToList();
            }

            var maxRating = 0;

            if (question is RatingQuestion ratingQuestion)
            {
                maxRating = ratingQuestion.MaxRating;
            }

            return new QuestionDbModel(question.IndexNumber, question.Text, question.QuestionType, maxRating, options);
        }

        private static SurveySubmitDbModel ToDbModel(SurveySubmit surveySubmit)
        {
            var answers = surveySubmit.Answers.Select(ToDbModel).ToList();

            return new SurveySubmitDbModel(surveySubmit.SubmitDateUtc, answers);
        }

        private static AnswerDbModel ToDbModel(BaseAnswer answer)
        {
            QuestionType questionType = QuestionType.FreeText;
            SelectedOptionDbModel? selectedOption = null;
            List<SelectedOptionDbModel> selectedOptions = new();
            int rating = 0;
            string? text = null;

            if (answer is SingleChoiceQuestionAnswer singleOptionAnswer)
            {
                questionType = QuestionType.SingleOption;
                selectedOption = new SelectedOptionDbModel(singleOptionAnswer.SelectedOption.IndexNumber);
            }

            if (answer is MultipleChoiceQuestionAnswer multipleOptionsAnswer)
            {
                questionType = QuestionType.MultipleOption;
                selectedOptions = multipleOptionsAnswer.SelectedOptions
                    .Select(option => new SelectedOptionDbModel(option.IndexNumber))
                    .ToList();
            }

            if (answer is RatingAnswer ratingAnswer)
            {
                questionType = QuestionType.Rating;
                rating = ratingAnswer.Rating;
            }

            if (answer is FreeTextAnswer commentAnswer)
            {
                questionType = QuestionType.FreeText;
                text = commentAnswer.Text;
            }

            return new AnswerDbModel(answer.QuestionIndexNumber, questionType, selectedOption, selectedOptions, rating,
                text);
        }

        public static Domain.Survey ToDomainModel(SurveyDbModel surveyDbModel)
        {
            var questions = surveyDbModel.Questions.Select(ToDomainModel).ToList();
            var submits = surveyDbModel.Submits.Select(ToDomainModel).ToList();

            return Domain.Survey.Restore(surveyDbModel.Id, surveyDbModel.Title, surveyDbModel.Description,
                surveyDbModel.ExpirationDateUtc, surveyDbModel.ResultAccessType, surveyDbModel.PinCode,
                questions, submits);
        }

        private static BaseQuestion ToDomainModel(QuestionDbModel questionDbModel)
        {
            return questionDbModel.QuestionType switch
            {
                QuestionType.SingleOption => new SingleChoiceQuestion(questionDbModel.IndexNumber,
                    questionDbModel.Text!,
                    new List<QuestionOption>(
                        questionDbModel.Options.Select(x => new QuestionOption(x.IndexNumber, x.Text!)))),
                QuestionType.MultipleOption => new MultipleChoiceQuestion(questionDbModel.IndexNumber,
                    questionDbModel.Text!,
                    new List<QuestionOption>(
                        questionDbModel.Options.Select(x => new QuestionOption(x.IndexNumber, x.Text!)))),
                QuestionType.Rating => new RatingQuestion(questionDbModel.IndexNumber, questionDbModel.Text!,
                    questionDbModel.MaxRating),
                QuestionType.FreeText => new FreeTextQuestion(questionDbModel.IndexNumber, questionDbModel.Text!),
                _ => throw new Exception($"Question Type {questionDbModel.QuestionType} is not allowed")
            };
        }

        private static SurveySubmit ToDomainModel(SurveySubmitDbModel surveySubmitDbModel)
        {
            var answers = surveySubmitDbModel.Answers.Select(ToDomainModel).ToList();

            return new SurveySubmit(surveySubmitDbModel.SubmitDateUtc, answers);
        }

        private static BaseAnswer ToDomainModel(AnswerDbModel answerDbModel)
        {
            return answerDbModel.QuestionType switch
            {
                QuestionType.SingleOption => new SingleChoiceQuestionAnswer(answerDbModel.QuestionIndexNumber,
                    new SelectedOption(answerDbModel.SelectedOption!.IndexNumber)),
                QuestionType.MultipleOption => new MultipleChoiceQuestionAnswer(answerDbModel.QuestionIndexNumber,
                    new List<SelectedOption>
                    (
                        answerDbModel.SelectedOptions.Select(x => new SelectedOption(x.IndexNumber)).ToList()
                    )),
                QuestionType.Rating => new RatingAnswer(answerDbModel.QuestionIndexNumber, answerDbModel.Rating),
                QuestionType.FreeText => new FreeTextAnswer(answerDbModel.QuestionIndexNumber, answerDbModel.Text!),
                _ => throw new Exception($"Question Type {answerDbModel.QuestionType} is not allowed")
            };
        }
    }
}
