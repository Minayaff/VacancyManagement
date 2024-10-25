using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacancyManagement.Application.Grading;
using VacancyManagement.Application.CandidateAnswers;
using WebApp;

namespace VacancyManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CandidateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CandidateController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(Config.GetApiUrl("grading/list"));

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var candidates = JsonConvert.DeserializeObject<List<CandidateResultDto>>(jsonResult);
                return View(candidates);
            }

            ViewBag.ErrorMessage = "Error fetching candidates data.";
            return View(new List<CandidateResultDto>());
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> AnswerDetails(int candidateId)
        {
            var client = _httpClientFactory.CreateClient();

            var apiUrl = Config.GetApiUrl($"candidate-answers/candidate-answer-details/{candidateId}");

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var candidateAnswerDetails = JsonConvert.DeserializeObject<CandidateAnswerDetailsDto>(jsonResult);
                    return View(candidateAnswerDetails);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error fetching candidate answer details.";
                    return View("Error");
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Request error: {ex.Message}";
                return View("Error");
            }
        }


    }
}
