using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Application.Grading.Queries
{
    public class GetAllCandidatesQuery : IRequest<List<CandidateResultDto>>
    {
    }
}
