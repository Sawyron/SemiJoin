using System.Collections.Immutable;

namespace SemiJoin.Domain;

public static class AttributeSetExtensions
{
    public static AttributeSet Create(IEnumerable<string> attributes) =>
        new FiniteAttributeSet(attributes.ToImmutableHashSet());

    public static AttributeSet CreateInfinite() => new InfiniteAttributeSet();

    public static AttributeSet Min(this AttributeSet that, AttributeSet other) =>
        (that, other) switch
        {
            (FiniteAttributeSet first, FiniteAttributeSet second) =>
                first.Attributes.Count <= second.Attributes.Count ? first : second,
            (InfiniteAttributeSet, FiniteAttributeSet set) => set,
            (FiniteAttributeSet set, InfiniteAttributeSet) => set,
            (InfiniteAttributeSet inf, InfiniteAttributeSet) => inf,
            _ => throw new InvalidOperationException("Unexpected AttributeSet type")
        };

    public static AttributeSet Merge(this AttributeSet that, AttributeSet other) =>
        (that, other) switch
        {
            (FiniteAttributeSet first, FiniteAttributeSet second) =>
                new FiniteAttributeSet(first.Attributes
                    .Concat(second.Attributes)
                    .ToImmutableHashSet()),
            (InfiniteAttributeSet inf, FiniteAttributeSet) => inf,
            (FiniteAttributeSet, InfiniteAttributeSet inf) => inf,
            (InfiniteAttributeSet inf, InfiniteAttributeSet) => inf,
            _ => throw new InvalidOperationException("Unexpected AttributeSet type")
        };

    public static string ToPrintableSting(this AttributeSet attributeSet) =>
        attributeSet switch
        {
            InfiniteAttributeSet => "∞",
            FiniteAttributeSet set when set.Attributes.Count == 0 => "0",
            FiniteAttributeSet set => string.Join('+', set.Attributes),
            _ => throw new InvalidOperationException("Unexpected AttributeSet type")
        };
}
