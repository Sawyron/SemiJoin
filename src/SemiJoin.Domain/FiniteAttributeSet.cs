namespace SemiJoin.Domain;

public record FiniteAttributeSet(IReadOnlySet<string> Attributes) : AttributeSet;
