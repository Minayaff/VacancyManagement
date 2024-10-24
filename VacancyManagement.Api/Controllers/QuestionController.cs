using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VacancyManagement.Application.Questions.Queries;
using VacancyManagement.Infrastructure;

namespace VacancyManagement.Api.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{vacancyId}")]
        public async Task<IActionResult> GetQuestions(int vacancyId)
        {
            try
            {
                var query = new GetQuestionsQuery(vacancyId);
                var questions = await _mediator.Send(query);

                if (questions == null || !questions.Any())
                {
                    return NotFound(new { message = "No questions found for the given vacancy." });
                }
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string jsonResult = JsonConvert.SerializeObject(questions, settings);
                return Ok(jsonResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
