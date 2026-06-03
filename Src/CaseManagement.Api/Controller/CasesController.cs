using CaseManagement.Application.Cases.Commands.CreateCase;
using CaseManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.Application.Cases.Queries.GetCaseById;
using CaseManagement.Application.Cases.Queries.GetAllCases;


namespace CaseManagement.Api.Controller
{
    [ApiController]
    [Route("api/cases")]
    public class CasesController : ControllerBase
    {
        private readonly CreateCaseCommandHandler _createCaseHandler;
        private readonly GetCaseByIdQueryHandler _getCaseByIdHandler;
        private readonly GetCasesQueryHandler _getCasesQueryHandler;

        public CasesController(CreateCaseCommandHandler createCaseHandler, GetCaseByIdQueryHandler getCaseByIdHandler, GetCasesQueryHandler getCasesQueryHandler)
        {
            _createCaseHandler = createCaseHandler;
            _getCaseByIdHandler = getCaseByIdHandler;
            _getCasesQueryHandler = getCasesQueryHandler;
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
        public async Task<IActionResult> GetCases([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 20,CancellationToken cancellationToken = default)
        {
            var query = new GetCasesQuery(pageNumber, pageSize);

            var response = await _getCasesQueryHandler.Handle(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCaseById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCaseByIdQuery(id);

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

