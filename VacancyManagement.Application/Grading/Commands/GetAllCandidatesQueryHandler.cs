using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyManagement.Application.Grading.Queries;
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Application.Grading.Commands
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, List<CandidateResultDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCandidatesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateResultDto>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {

            var candidates = await _context.Candidates
                .Include(c => c.CandidateAnswers)
                    .ThenInclude(ca => ca.Question)
                    .ThenInclude(c => c.Vacancy)
                .Include(c => c.CandidateAnswers)
                    .ThenInclude(ca => ca.AnswerOption)
                .Include(c => c.CVFile)
                .ToListAsync(cancellationToken);

            // Mapping to DTOs
            var result = candidates.Select(c => new CandidateResultDto
            {
                CandidateId = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                ApplicationDate = c.ApplicationDate,
                TotalQuestions = c.CandidateAnswers.Count(),
                CorrectAnswers = c.CandidateAnswers.Count(ca => ca.IsCorrect),
                Percentage = c.CandidateAnswers.Count() > 0 ?
                             (double)c.CandidateAnswers.Count(ca => ca.IsCorrect) / c.CandidateAnswers.Count() * 100 : 0,
                CVFileName = c.CVFile?.FileName,
                CVDownloadUrl = c.CVFile != null ?
                        $"data:{c.CVFile.ContentType};base64,{c.CVFile.Base64Data}" : null,
                 VacancyName = c.CandidateAnswers.FirstOrDefault().Question.Vacancy.Title // Fetch Vacancy Name
            }).ToList();

            return result;
        }
    }

}
