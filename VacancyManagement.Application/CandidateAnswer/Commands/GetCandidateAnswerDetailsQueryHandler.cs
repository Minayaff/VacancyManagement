using MediatR;
using Microsoft.EntityFrameworkCore;
using VacancyManagement.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VacancyManagement.Application.CandidateAnswers.Queries
{
    public class GetCandidateAnswerDetailsQueryHandler : IRequestHandler<GetCandidateAnswerDetailsQuery, CandidateAnswerDetailsDto>
    {
        private readonly ApplicationDbContext _context;

        public GetCandidateAnswerDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateAnswerDetailsDto> Handle(GetCandidateAnswerDetailsQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates
                .Include(c => c.CandidateAnswers)
                    .ThenInclude(a => a.Question)
                .Include(c => c.CandidateAnswers)
                    .ThenInclude(a => a.AnswerOption)
                .FirstOrDefaultAsync(c => c.Id == request.CandidateId, cancellationToken);

            if (candidate == null)
            {
                return null;
            }

            return new CandidateAnswerDetailsDto
            {
                CandidateName = candidate.FullName,
                Answers = candidate.CandidateAnswers.Select(a => new AnswerDetail
                {
                    QuestionText = a.Question.Text,
                    SelectedOptionText = a.AnswerOption?.Text ?? "Cavablanmayıb", // Show "Not Answered" if no option selected
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }
    }
}
