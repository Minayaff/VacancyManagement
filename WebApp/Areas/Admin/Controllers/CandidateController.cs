using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacancyManagement.Application.Grading;

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
            var response = await client.GetAsync("https://localhost:7298/api/grading/list");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var candidates = JsonConvert.DeserializeObject<List<CandidateResultDto>>(jsonResult);
                return View(candidates);
            }

            ViewBag.ErrorMessage = "Error fetching candidates data.";
            return View(new List<CandidateResultDto>());
        }
    }
}
