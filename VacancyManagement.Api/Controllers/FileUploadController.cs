using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VacancyManagement.Application.Candidate.Commands;
using VacancyManagement.Application.CandidateFile;

namespace VacancyManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/FileUpload/candidate-cv
        [HttpPost("candidate-cv")]
        public async Task<IActionResult> UploadCandidateCv([FromBody] UploadCandidateCvCommand command)
        {
            if (command == null)
            {
                return BadRequest("Tələb olunan məlumat yoxdur.");
            }

            try
            {
                var fileEntityId = await _mediator.Send(command);

                return Ok(new { FileEntityId = fileEntityId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server xətası: {ex.Message}");
            }
        }
    }
}
