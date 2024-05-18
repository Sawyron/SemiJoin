using SemiJoin.Domain;
using System.Text;

namespace SemiJoin;

public static class SemiJoinExtensions
{
    public static AttributeSet[,] MapToAttributeMatrix(this MatrixDto matrix)
    {
        var keys = matrix.Value.Keys.ToList();
        var result = new AttributeSet[keys.Count, keys.Count];
        for (int i = 0; i < keys.Count; i++)
        {
            List<string> values = matrix.Value[keys[i]];
            for (int j = 0; j < keys.Count; j++)
            {
                string current = values[j].Trim();
                result[i, j] = current switch
                {
                    string when string.Equals("inf", current, StringComparison.OrdinalIgnoreCase) =>
                        AttributeSetExtensions.CreateInfinite(),
                    string when string.IsNullOrWhiteSpace(current) =>
                        AttributeSetExtensions.Create([]),
                    _ => AttributeSetExtensions.Create(current.Split(" "))
                };
            }
        }
        return result;
    }

    public static string ToPrintableString(
        this AttributeSet[,] matrix,
        IEnumerable<string> headers)
    {
        var sb = new StringBuilder();
        var headerList = headers.ToList();
        sb.Append(' ', 4);
        sb.AppendJoin(" | ", headerList);
        sb.AppendLine();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            sb.Append(headerList[i]);
            sb.Append(" | ");
            sb.AppendJoin(", ",
                Enumerable.Range(0, matrix.GetLength(1))
                    .Select(j => matrix[i, j])
                    .Select(s => s.ToPrintableSting()));
            sb.AppendLine();
        }
        return sb.ToString();
    } 
}
