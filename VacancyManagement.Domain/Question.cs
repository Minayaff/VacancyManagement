using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public List<AnswerOption> Options { get; set; }

        public int VacancyId { get; set; }  
        public Vacancy Vacancy { get; set; }

        public int CorrectOptionId { get; set; }
        public List<CandidateAnswers> CandidateAnswers { get; set; } = new List<CandidateAnswers>();

    }
}
