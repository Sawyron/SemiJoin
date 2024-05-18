namespace SemiJoin.Domain;

public class SemiJoinMinimizer
{
    public AttributeSet[,] Minimize(AttributeSet[,] matrix)
    {
        var result = new AttributeSet[matrix.GetLength(0), matrix.GetLength(1)];
        Array.Copy(matrix, result, matrix.Length);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                AttributeSet set = AttributeSetExtensions.CreateInfinite();
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    var first = matrix[i, k];
                    var second = matrix[k, j];
                    set = set.Min(first.Merge(second));
                }
                result[i, j] = set;
            }
        }
        return result;
    }
}
