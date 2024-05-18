namespace SemiJoin.Domain.Test;

public class SemiJoinMinimizerTest
{
    [Fact]
    public void TestMinimize()
    {
        var mimimizer = new SemiJoinMinimizer();
        var matrix = new AttributeSet[,]
        {
            {
                AttributeSetExtensions.Create([]),
                AttributeSetExtensions.Create(["id1"]),
                AttributeSetExtensions.Create(["id3"]),
                AttributeSetExtensions.CreateInfinite(),
            },
            {
                AttributeSetExtensions.Create(["id1"]),
                AttributeSetExtensions.Create([]),
                AttributeSetExtensions.Create(["id2"]),
                AttributeSetExtensions.CreateInfinite(),
            },
            {
                AttributeSetExtensions.Create(["id3"]),
                AttributeSetExtensions.Create(["id2"]),
                AttributeSetExtensions.Create([]),
                AttributeSetExtensions.Create(["id4"]),
            },
            {
                AttributeSetExtensions.CreateInfinite(),
                AttributeSetExtensions.CreateInfinite(),
                AttributeSetExtensions.Create(["id4"]),
                AttributeSetExtensions.Create([]),
            },
        };
        var result = mimimizer.Minimize(matrix);
        Assert.True((result[0, 0] as FiniteAttributeSet)!.Attributes.SetEquals([]));
        Assert.True((result[0, 1] as FiniteAttributeSet)!.Attributes.SetEquals(["id1"]));
        Assert.True((result[0, 2] as FiniteAttributeSet)!.Attributes.SetEquals(["id3"]));
        Assert.True((result[0, 3] as FiniteAttributeSet)!.Attributes.SetEquals(["id3", "id4"]));
        Assert.True((result[1, 0] as FiniteAttributeSet)!.Attributes.SetEquals(["id1"]));
        Assert.True((result[1, 1] as FiniteAttributeSet)!.Attributes.SetEquals([]));
        Assert.True((result[1, 2] as FiniteAttributeSet)!.Attributes.SetEquals(["id2"]));
        Assert.True((result[1, 3] as FiniteAttributeSet)!.Attributes.SetEquals(["id2", "id4"]));
        Assert.True((result[2, 0] as FiniteAttributeSet)!.Attributes.SetEquals(["id3"]));
        Assert.True((result[2, 1] as FiniteAttributeSet)!.Attributes.SetEquals(["id2"]));
        Assert.True((result[2, 2] as FiniteAttributeSet)!.Attributes.SetEquals([]));
        Assert.True((result[2, 3] as FiniteAttributeSet)!.Attributes.SetEquals(["id4"]));
        Assert.True((result[3, 0] as FiniteAttributeSet)!.Attributes.SetEquals(["id3", "id4"]));
        Assert.True((result[3, 1] as FiniteAttributeSet)!.Attributes.SetEquals(["id2", "id4"]));
        Assert.True((result[3, 2] as FiniteAttributeSet)!.Attributes.SetEquals(["id4"]));
        Assert.True((result[3, 3] as FiniteAttributeSet)!.Attributes.SetEquals([]));
    }
}
