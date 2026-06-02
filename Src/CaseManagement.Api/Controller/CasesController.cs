using CaseManagement.Application.Cases.Commands.CreateCase;
using CaseManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.Application.Cases.Queries.GetCaseById;


namespace CaseManagement.Api.Controller
{
    [ApiController]
    [Route("api/cases")]
    public class CasesController : ControllerBase
    {
        private readonly CreateCaseCommandHandler _createCaseHandler;
        private readonly GetCaseByIdQueryHandler _getCaseByIdHandler;

        public CasesController(CreateCaseCommandHandler createCaseHandler, GetCaseByIdQueryHandler getCaseByIdHandler)
        {
            _createCaseHandler = createCaseHandler;
            _getCaseByIdHandler = getCaseByIdHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase(CreateCaseRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateCaseCommand(
                request.CaseNumber,
                request.Title,
                request.Description,
                request.Priority,
                request.CategoryId);

            var caseId = await _createCaseHandler.Handle(command, cancellationToken);

            return CreatedAtAction(nameof(GetCaseById), new { id = caseId }, new { Id = caseId });
        }

        [HttpGet]
        public async Task<IActionResult> GetCases()
        {
            // Implementer logik for at hente sager her
            return Ok();


        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCaseById(Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCaseByIdQuery(Id);

            var response = await _getCaseByIdHandler.Handle(query, cancellationToken);
    
            return Ok(response);
        }

    }
        public sealed record CreateCaseRequest(

              string CaseNumber,
              string Title,
              string Description,
              CasePriority Priority,
              Guid? CategoryId);
        
}

