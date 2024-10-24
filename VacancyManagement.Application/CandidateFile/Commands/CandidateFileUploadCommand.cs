using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VacancyManagement.Application.CandidateFile;
using VacancyManagement.Application.Services;
using VacancyManagement.Domain;
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Application.Candidate.Commands
{
    public class CandidateFileUploadCommand  : IRequestHandler<UploadCandidateCvCommand, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public CandidateFileUploadCommand(ApplicationDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<int> Handle(UploadCandidateCvCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(new object[] { request.CandidateId }, cancellationToken);
            if (candidate == null)
            {
                throw new Exception("Candidate not found.");
            }

            var fileEntity = await _fileUploadService.UploadCvAsync(request);


            await _context.FileEntity.AddAsync(fileEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            candidate.FileEntityId = fileEntity.Id;
            try
            {

            await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }

            return fileEntity.Id; 
        }
    }
}
