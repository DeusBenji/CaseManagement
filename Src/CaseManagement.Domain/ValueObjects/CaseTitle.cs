using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.ValueObjects;

public sealed class CaseTitle : ValueObject
{
    public string Value { get; }

    private CaseTitle()
    {
        Value = null!;
    }

    public CaseTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Titel er påkrævet.", nameof(value));

        var trimmed = value.Trim();

        if (trimmed.Length > 200)
            throw new ArgumentException("Titel må maks være 200 tegn.", nameof(value));

        Value = trimmed;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(CaseTitle title) => title.Value;
}