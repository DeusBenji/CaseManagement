using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Domain.Entities;
using CaseManagement.Domain.ValueObjects;

namespace CaseManagement.Application.Cases.Commands.CreateCase;

public sealed class CreateCaseCommandHandler
{
    private readonly ICaseRepository _caseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCaseCommandHandler(
        ICaseRepository caseRepository,
        IUnitOfWork unitOfWork)
    {
        _caseRepository = caseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateCaseCommand command,
        CancellationToken cancellationToken = default)
    {
        var exists = await _caseRepository.ExistsByCaseNumberAsync(
            command.CaseNumber,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException("En sag med dette sagsnummer findes allerede.");

        var caseEntity = new Case(
            new CaseNumber(command.CaseNumber),
            new CaseTitle(command.Title),
            command.Description,
            command.Priority,
            command.CategoryId);

        await _caseRepository.AddAsync(caseEntity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return caseEntity.Id;
    }
}