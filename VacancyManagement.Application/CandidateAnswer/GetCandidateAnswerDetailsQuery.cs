using MediatR;
using System.Collections.Generic;

namespace VacancyManagement.Application.CandidateAnswers.Queries
{
    public class GetCandidateAnswerDetailsQuery : IRequest<CandidateAnswerDetailsDto>
    {
        public int CandidateId { get; set; }
    }
}
