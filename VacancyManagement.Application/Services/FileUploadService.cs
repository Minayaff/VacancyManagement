using System;
using System.IO;
using System.Threading.Tasks;
using VacancyManagement.Application.CandidateFile;
using VacancyManagement.Domain;

namespace VacancyManagement.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        public Task<FileEntity> UploadCvAsync(UploadCandidateCvCommand request)
        {
            byte[] fileBytes = Convert.FromBase64String(request.Base64File);

            string filePath = Path.Combine("Uploads", $"{Guid.NewGuid()}_{request.FileName}");

            // File.WriteAllBytes(filePath, fileBytes); // Uncomment this if writing to disk

            var fileEntity = new FileEntity
            {
                FileName = request.FileName,
                ContentType = request.ContentType,
                Base64Data = request.Base64File,  // Store the base64 string
                FileSize = request.FileSize,
            };

            return Task.FromResult(fileEntity);
        }
    }
}
