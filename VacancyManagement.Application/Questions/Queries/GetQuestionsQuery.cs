using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyManagement.Domain;
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Application.Questions.Queries
{
    public class GetQuestionsQuery : IRequest<List<Question>>
    {
        public int VacancyId { get; set; }

        public GetQuestionsQuery(int vacancyId)
        {
            VacancyId = vacancyId;
        }
    }

    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, List<Question>>
    {
        private readonly ApplicationDbContext _context;

        public GetQuestionsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions
                .Include(q => q.Options)
                .Where(q => q.VacancyId == request.VacancyId)
                .ToListAsync(cancellationToken);

        }
    }
}
