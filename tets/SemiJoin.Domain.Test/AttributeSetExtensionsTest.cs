namespace SemiJoin.Domain.Test;

public class AttributeSetExtensionsTest
{
    [Fact]
    public void TestMerge_WhenTwoAreFinite()
    {
        var first = AttributeSetExtensions.Create(["a", "b", "c"]);
        var second = AttributeSetExtensions.Create(["c", "d", "e"]);
        var result = first.Merge(second);
        Assert.IsType<FiniteAttributeSet>(result);
        var attributes = (result as FiniteAttributeSet)!.Attributes;
        Assert.Equal(5, attributes.Count);
        Assert.Contains("a", attributes);
        Assert.Contains("b", attributes);
        Assert.Contains("c", attributes);
        Assert.Contains("d", attributes);
        Assert.Contains("e", attributes);
    }

    [Fact]
    public void TestMerge_WhenOneIsInfinite()
    {
        var first = AttributeSetExtensions.Create(["a", "b"]);
        var second = AttributeSetExtensions.CreateInfinite();
        var result = first.Merge(second);
        Assert.IsType<InfiniteAttributeSet>(result);
    }

    [Fact]
    public void TestMin_WhenTwoAreFinite()
    {
        var first = AttributeSetExtensions.Create(["a", "b", "c"]);
        var second = AttributeSetExtensions.Create(["c", "d"]);
        var result = first.Min(second);
        Assert.Equal(second, result);
    }

    [Fact]
    public void TestMin_WhenOneIsInfinite()
    {
        var first = AttributeSetExtensions.Create(["a", "b", "c"]);
        var second = AttributeSetExtensions.CreateInfinite();
        var result = first.Min(second);
        Assert.Equal(first, result);
    }
}