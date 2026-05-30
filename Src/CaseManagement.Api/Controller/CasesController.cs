using CaseManagement.Application.Cases.Commands.CreateCase;
using CaseManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;


namespace CaseManagement.Api.Controller
{
    [ApiController]
    [Route("api/cases")]
    public class CasesController : ControllerBase
    {
        private readonly CreateCaseCommandHandler _handler;

        public CasesController(CreateCaseCommandHandler handler)
        { 
         _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase(CreateCaseRequest request, CancellationToken cancellationtoken)
        {
            var command = new CreateCaseCommand(
                request.CaseNumber,
                request.Title,
                request.Description,
                request.Priority,
                request.CategoryId);

            var caseId = await _handler.Handle(command, cancellationtoken);

            return CreatedAtAction(nameof(CreateCase), new { id = caseId }, new
            {
                Id = caseId
            });
        }

        }

        public sealed record CreateCaseRequest(

              string CaseNumber,
              string Title,
              string Description,
              CasePriority Priority,
              Guid? CategoryId);
        
}

