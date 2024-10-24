using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyManagement.Domain
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64Data { get; set; } 
        public long FileSize { get; set; }
    }

}
