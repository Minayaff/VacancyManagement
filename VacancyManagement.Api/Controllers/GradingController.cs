using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacancyManagement.Application.Grading.Queries;
using VacancyManagement.Application.Grading;

namespace VacancyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // API endpoint to get all candidates with their grading
        [HttpGet("list")]
        public async Task<ActionResult<List<CandidateResultDto>>> GetCandidateResults()
        {
            var candidates = await _mediator.Send(new GetAllCandidatesQuery());
            return Ok(candidates);
        }
    }
}
