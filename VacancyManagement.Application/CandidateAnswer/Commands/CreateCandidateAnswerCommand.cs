using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VacancyManagement.Domain;
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Application.CandidateAnswers.Commands
{
    public class CreateCandidateAnswerCommand : IRequest<int>
    {
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
        public bool IsCorrect { get; set; }

        public class Handler : IRequestHandler<CreateCandidateAnswerCommand, int>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCandidateAnswerCommand request, CancellationToken cancellationToken)
            {
                // Validate that Candidate, Question, and AnswerOption exist
                var candidate = await _context.Candidates.FindAsync(new object[] { request.CandidateId }, cancellationToken);
                if (candidate == null)
                {
                    throw new Exception("Candidate not found");
                }

                var question = await _context.Questions.FindAsync(new object[] { request.QuestionId }, cancellationToken);
                if (question == null)
                {
                    throw new Exception("Question not found");
                }

                var answerOption = await _context.AnswerOptions.FindAsync(new object[] { request.AnswerOptionId }, cancellationToken);
                if (answerOption == null)
                {
                    throw new Exception("AnswerOption not found");
                }

                // Create a new CandidateAnswer
                var candidateAnswer = new Domain.CandidateAnswers
                {
                    CandidateId = request.CandidateId,
                    QuestionId = request.QuestionId,
                    AnswerOptionId = request.AnswerOptionId,
                    IsCorrect = request.IsCorrect,
                    AnswerDate = DateTime.Now
                };

                // Add and save changes to the database
                await _context.CandidateAnswers.AddAsync(candidateAnswer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return candidateAnswer.Id;
            }
        }
    }
}
