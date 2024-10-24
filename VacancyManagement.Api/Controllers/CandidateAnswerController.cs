using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VacancyManagement.Application.CandidateAnswers.Commands;

namespace VacancyManagement.Api.Controllers
{
    [Route("api/candidate-answers")]
    [ApiController]
    public class CandidateAnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateAnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateAnswerCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
