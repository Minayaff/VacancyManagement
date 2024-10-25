using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VacancyManagement.Domain;

namespace WebApp.Controllers
{
    public class VacancyController : Controller
    {
        private readonly HttpClient _httpClient;

        public VacancyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(Config.Api); 
        }

        // GET: /Vacancy
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/vacancies");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var vacancies = JsonConvert.DeserializeObject<List<Vacancy>>(jsonResponse);
                return View(vacancies);
            }
            else
            {
                ViewBag.ErrorMessage = "Vakansiyaları yükləyərkən xəta baş verdi.";
                return View(new List<Vacancy>());
            }
        }

        public IActionResult Apply(int id)
        {
            ViewBag.VacancyId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(Candidate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _httpClient.PostAsJsonAsync(Config.GetApiUrl("candidates"), model);

            if (response.IsSuccessStatusCode)
            {
                var candidateId = await response.Content.ReadAsStringAsync();
                int candidateIdValue = int.Parse(candidateId);
                TempData["SuccessMessage"] = "Müraciətiniz uğurla göndərildi!";
                return RedirectToAction("Start", "Quiz", new { vacancyId = model.VacancyId, candidateId = candidateIdValue });
            }
            else
            {
                ViewBag.ErrorMessage = "Müraciət zamanı xəta baş verdi.";
                return View(model);
            }
        }


         [HttpGet("vacancy/uploadCv/{candidateId}")]
        public IActionResult UploadCv(int candidateId)
        {
            ViewBag.CandidateId = candidateId;
            return View();
        }


        // POST: Candidate/UploadCv
        [HttpPost("vacancy/uploadCv/{candidateId}")]
        public async Task<IActionResult> UploadCv(IFormFile file, int candidateId)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.ErrorMessage = "Fayl seçilməyib.";
                ViewBag.CandidateId = candidateId;
                return View();
            }

            // Validate file size (Max 5MB)
            if (file.Length > 5 * 1024 * 1024)
            {
                ViewBag.ErrorMessage = "Maksimum fayl ölçüsü 5MB-dır.";
                ViewBag.CandidateId = candidateId;
                return View();
            }

            // Validate file format (PDF or DOCX)
            var allowedExtensions = new[] { ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (Array.IndexOf(allowedExtensions, fileExtension) < 0)
            {
                ViewBag.ErrorMessage = "Qəbul olunan fayl formatları: PDF və ya DOCX.";
                ViewBag.CandidateId = candidateId;
                return View();
            }

            // Convert file to Base64
            string base64File;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();
                base64File = Convert.ToBase64String(fileBytes);
            }

            // Create the object to send to the API
            var command = new
            {
                CandidateId = candidateId,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Base64File = base64File,
                FileSize = file.Length
            };

            try
            {
                // Send the CV data to the API as JSON
                var response = await _httpClient.PostAsJsonAsync(Config.GetApiUrl("FileUpload/candidate-cv"), command);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "CV uğurla yükləndi!";
                    ViewBag.CandidateId = candidateId;
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "CV yüklənməsi zamanı xəta baş verdi.";
                    ViewBag.CandidateId = candidateId;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Xəta baş verdi: {ex.Message}";
                ViewBag.CandidateId = candidateId;
                return View();
            }
        }
    }
}
