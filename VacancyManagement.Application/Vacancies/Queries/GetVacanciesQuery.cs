using System;
using MediatR;
using Microsoft.EntityFrameworkCore; 
using VacancyManagement.Domain; 
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Application.Vacancies.Queries
{
    public class GetVacanciesQuery : IRequest<List<Vacancy>> { }

    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, List<Vacancy>>
    {
        private readonly ApplicationDbContext _context;

        public GetVacanciesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vacancy>> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Vacancies.Include(v => v.Questions).ThenInclude(x=>x.Options).Where(v => v.IsActive).ToListAsync(cancellationToken);
        }
    }
}
