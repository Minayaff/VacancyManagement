using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VacancyManagement.Application.Vacancies.Queries;

namespace VacancyManagement.Api.Controllers
{
    [Route("api/vacancies")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VacancyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vacancies = await _mediator.Send(new GetVacanciesQuery());
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string jsonResult = JsonConvert.SerializeObject(vacancies, settings);
            return Ok(jsonResult);
        }
    }
}
