using System.Collections.Generic;

namespace VacancyManagement.Application.CandidateAnswers
{
    public class CandidateAnswerDetailsDto
    {
        public string CandidateName { get; set; }
        public List<AnswerDetail> Answers { get; set; } = new List<AnswerDetail>();
    }

    public class AnswerDetail
    {
        public string QuestionText { get; set; }
        public string SelectedOptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
