using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VacancyManagement.Application.CandidateAnswers.Commands;
using VacancyManagement.Application.Grading.Queries;
using VacancyManagement.Application.Grading;
using VacancyManagement.Application.CandidateAnswers;
using VacancyManagement.Application.CandidateAnswers.Queries;

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
        [HttpGet("candidate-answer-details/{candidateId}")]
        public async Task<ActionResult<CandidateAnswerDetailsDto>> GetCandidateAnswerDetails(int candidateId)
        {
            var query = new GetCandidateAnswerDetailsQuery { CandidateId = candidateId };
            var candidateAnswerDetails = await _mediator.Send(query);

            if (candidateAnswerDetails == null)
            {
                return NotFound(new { Message = "Candidate not found or no answers available." });
            }

            return Ok(candidateAnswerDetails);
        }
    }
}
