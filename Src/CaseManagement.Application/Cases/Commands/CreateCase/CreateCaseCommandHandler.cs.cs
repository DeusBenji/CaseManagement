using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Application.Common.Exceptions;
using CaseManagement.Domain.Entities;
using CaseManagement.Domain.ValueObjects;
using CaseManagement.Domain.Enums;

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
        if (string.IsNullOrWhiteSpace(command.Description))
            throw new RequestValidationException("Beskrivelse må ikke være tom.");

        if (!Enum.IsDefined(typeof(CasePriority), command.Priority))
            throw new RequestValidationException("Ugyldig prioritet.");

        if (command.CategoryId.HasValue && command.CategoryId.Value == Guid.Empty)
            throw new RequestValidationException("KategoriId må ikke være tomt.");

        var exists = await _caseRepository.ExistsByCaseNumberAsync(
            command.CaseNumber,
            cancellationToken);

        if (exists)
            throw new ConflictException("En sag med dette sagsnummer findes allerede.");

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