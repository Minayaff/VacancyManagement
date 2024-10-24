using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Domain
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad və soyad tələb olunur.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-poçt ünvanı tələb olunur.")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt formatı daxil edin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi tələb olunur.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Telefon nömrəsi '055-928-87-55' formatında olmalıdır.")]
        public string PhoneNumber { get; set; }

        public int VacancyId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int? FileEntityId { get; set; }
        public FileEntity? CVFile { get; set; }
        public List<CandidateAnswers> CandidateAnswers { get; set; } = new List<CandidateAnswers>();
    }
}
