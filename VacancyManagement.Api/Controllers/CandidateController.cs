using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacancyManagement.Application.Candidates.Commands;
using VacancyManagement.Domain;

namespace VacancyManagement.Api.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
