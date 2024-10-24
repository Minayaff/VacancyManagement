using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Domain
{
    public class CandidateAnswers
    {
        public int Id { get; set; } // Primary Key
        public int CandidateId { get; set; } // Foreign Key referencing Candidate
        public Candidate Candidate { get; set; }

        public int QuestionId { get; set; } // Foreign Key referencing Question
        public Question Question { get; set; }

        public int AnswerOptionId { get; set; } // Foreign Key referencing AnswerOption
        public AnswerOption AnswerOption { get; set; }


        public bool IsCorrect { get; set; } // Indicates if the answer is correct
        public DateTime AnswerDate { get; set; } // Date and time when answer is submitted

    }
}
