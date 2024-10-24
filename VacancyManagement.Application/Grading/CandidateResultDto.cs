using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Application.Grading
{
    public class CandidateResultDto
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ApplicationDate { get; set; }

        // Test result fields
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double Percentage { get; set; }

        // CV Information
        public string CVFileName { get; set; }  // Name or URL of the CV file
        public string CVDownloadUrl { get; set; }  // URL to download the CV
        public string VacancyName { get; set; }
    }

}
