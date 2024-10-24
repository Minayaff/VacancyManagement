using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Application.CandidateFile
{
    public class UploadCandidateCvCommand : IRequest<int>
    {
        public int CandidateId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64File { get; set; }
        public long FileSize { get; set; }
    }
}
