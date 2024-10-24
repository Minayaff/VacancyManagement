using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Domain
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsCorrect { get; set; }
        public List<CandidateAnswers> CandidateAnswers { get; set; } = new List<CandidateAnswers>();
    }
}
