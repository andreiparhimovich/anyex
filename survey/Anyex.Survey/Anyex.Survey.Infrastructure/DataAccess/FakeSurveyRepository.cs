using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Domain;
using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Infrastructure.DataAccess
{
    public class FakeSurveyRepository : IRepository<Domain.Survey, string>
    {
        private readonly List<BaseQuestion> _questions = new();
        private readonly List<BaseAnswer> _answers = new();

        private static List<Domain.Survey> Surveys;

        static FakeSurveyRepository()
        {
            Surveys = new List<Domain.Survey>();
        }

        public FakeSurveyRepository()
        {
            var multipleOptionsQuestion = new MultipleChoiceQuestion(1, "What place are we going to go?",
                new List<QuestionOption>
                {
                    new(1, "Place #1"),
                    new(2, "Place #2")
                });

            var singleOptionsQuestion = new SingleChoiceQuestion(2, "What would you choose?", new List<QuestionOption>
            {
                new(1, "Yes"),
                new(2, "No")
            });

            var commentQuestion = new FreeTextQuestion(3, "What do you think?");

            var starsQuestion = new RatingQuestion(4, "How hot is it here now", 10);

            _questions.Add(multipleOptionsQuestion);
            _questions.Add(singleOptionsQuestion);
            _questions.Add(commentQuestion);
            _questions.Add(starsQuestion);

            var multipleAnswer = new MultipleChoiceQuestionAnswer(1,
                new List<SelectedOption>
                {
                    new(1),
                    new(2)
                });

            var singleAnswer = new SingleChoiceQuestionAnswer(2, new SelectedOption(1));

            var commentAnswer = new FreeTextAnswer(3, "I think it's very important");

            var starsAnswer = new RatingAnswer(4, 7);

            _answers.Add(multipleAnswer);
            _answers.Add(singleAnswer);
            _answers.Add(commentAnswer);
            _answers.Add(starsAnswer);
        }

        public Task<Domain.Survey> CreateAsync(Domain.Survey model)
        {
            var existingSurvey = Surveys.Find(survey => survey.Id == model.Id);

            if (existingSurvey is not null)
            {
                Surveys.Remove(existingSurvey);
            }
            
            Surveys.Add(model);

            return Task.FromResult(model);
        }

        public Task<Domain.Survey?> GetByIdAsync(string id)
        {
            return Task.FromResult(Surveys.Find(survey => survey.Id == id));
        }

        public Task UpdateAsync(Domain.Survey model)
        {
            var existingSurvey = Surveys.Find(survey => survey.Id == model.Id);

            if (existingSurvey is not null)
            {
                Surveys.Remove(existingSurvey);
            }
            
            Surveys.Add(model);

            return Task.FromResult(model);
        }
    }
}
