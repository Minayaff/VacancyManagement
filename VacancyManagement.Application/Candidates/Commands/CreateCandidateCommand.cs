using MediatR;
using VacancyManagement.Domain;
using VacancyManagement.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using VacancyManagement.Application.Services;

namespace VacancyManagement.Application.Candidates.Commands
{
    public class CreateCandidateCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int VacancyId { get; set; }
        public DateTime ApplicationDate { get; set; }=DateTime.Now; 
        public int? FileEntityId { get; set; }
        public FileEntity? CVFile { get; set; }



    }
    public class Handler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public Handler(ApplicationDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new VacancyManagement.Domain.Candidate
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                VacancyId = request.VacancyId,
                ApplicationDate = DateTime.Now,
                CVFile = null
            };


            await _context.Candidates.AddAsync(candidate, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return candidate.Id;
        }
    }
}
