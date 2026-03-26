using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.ValueObjects;

public sealed class CaseNumber : ValueObject
{
    public string Value { get; }

    private CaseNumber()
    {
        Value = null!;
    }

    public CaseNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Sagsnummer er påkrævet.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > 50)
            throw new ArgumentException("Sagsnummer må maks være 50 tegn.", nameof(value));

        Value = trimmed;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(CaseNumber caseNumber) => caseNumber.Value;
}