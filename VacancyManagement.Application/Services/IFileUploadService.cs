using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyManagement.Application.CandidateFile;
using VacancyManagement.Domain;

namespace VacancyManagement.Application.Services
{
    public interface IFileUploadService
    {
        Task<FileEntity> UploadCvAsync(UploadCandidateCvCommand request);
    }
}
