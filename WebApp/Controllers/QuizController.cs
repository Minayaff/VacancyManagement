using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using VacancyManagement.Domain;

namespace WebApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QuizController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("quiz/instructions/{vacancyId}/{candidateId}")]
        public IActionResult Instructions(int vacancyId, int candidateId)
        {
            ViewBag.VacancyId = vacancyId;
            ViewBag.CandidateId = candidateId;
            return View();
        }

        [HttpGet("quiz/start/{vacancyId}/{candidateId}")]
        public async Task<IActionResult> Start(int vacancyId, int candidateId)
        {
            if (vacancyId == 0 || candidateId == 0)
            {
                ViewBag.ErrorMessage = "Vakansiya ID və ya namizəd ID düzgün deyil.";
                return View("Error");
            }
            var client = _httpClientFactory.CreateClient();
            string apiUrl = $"https://localhost:7298/api/questions/{vacancyId}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Vakansiya üçün suallar əldə edilə bilmədi.";
                    return View("Error");
                }

                string jsonString = await response.Content.ReadAsStringAsync();
                var questions = JsonConvert.DeserializeObject<List<Question>>(jsonString);

                if (questions == null || !questions.Any())
                {
                    ViewBag.ErrorMessage = "Bu vakansiya üçün heç bir sual tapılmadı.";
                    return View("Error");
                }

                // Store questions in TempData for later use
                TempData["Questions"] = JsonConvert.SerializeObject(questions);
                TempData["CurrentQuestionIndex"] = 0;
                TempData["CandidateId"] = candidateId;
                TempData.Keep();

                ViewBag.QuestionCount = questions.Count;
                return RedirectToAction("NextQuestion");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Xəta baş verdi: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet("quiz/next")]
        public IActionResult NextQuestion()
        {
            var questionsJson = TempData["Questions"] as string;
            if (string.IsNullOrEmpty(questionsJson))
            {
                return RedirectToAction("Complete");
            }

            var questions = JsonConvert.DeserializeObject<List<Question>>(questionsJson);
            int currentQuestionIndex = (int)TempData["CurrentQuestionIndex"];
            int candidateId = (int)TempData["CandidateId"];

            if (currentQuestionIndex >= questions.Count)
            {
                return RedirectToAction("Complete");
            }

            var question = questions[currentQuestionIndex];
            TempData["CurrentQuestionIndex"] = currentQuestionIndex + 1;
            TempData["Questions"] = JsonConvert.SerializeObject(questions);
            TempData["CandidateId"] = candidateId;
            TempData.Keep(); // Keep TempData across requests

            ViewBag.CandidateId = candidateId;
            return View("Question", question);
        }

        [HttpPost("quiz/submit-answer")]
        public async Task<IActionResult> SubmitAnswer(int questionId, int selectedOptionId, int vacancyId, int candidateId)
        {
            var questionsJson = TempData["Questions"] as string;
            if (string.IsNullOrEmpty(questionsJson))
            {
                return RedirectToAction("Complete");
            }
            var questions = JsonConvert.DeserializeObject<List<Question>>(questionsJson);
            int currentQuestionIndex = (int)TempData["CurrentQuestionIndex"] - 1;

            if (currentQuestionIndex < 0 || currentQuestionIndex >= questions.Count)
            {
                return RedirectToAction("Complete");
            }

            var question = questions[currentQuestionIndex];
            bool isCorrect = question.Options.Any(o => o.Id == selectedOptionId && o.IsCorrect);

            // Prepare the command to create candidate answer
            var command = new CandidateAnswers
            {
                CandidateId = candidateId,
                QuestionId = questionId,
                AnswerOptionId = selectedOptionId,
                IsCorrect = isCorrect
            };

            var client = _httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:7298/api/candidate-answers";

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, command);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Cavab göndərilmə zamanı xəta baş verdi.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Xəta baş verdi: {ex.Message}";
                return View("Error");
            }

            return RedirectToAction("NextQuestion");
        }

        [HttpGet("quiz/complete")]
        public IActionResult Complete()
        {
            int candidateId = (int)TempData["CandidateId"]; 
            return RedirectToAction("UploadCv", "Vacancy", new { candidateId = candidateId });
        }
    }
}
