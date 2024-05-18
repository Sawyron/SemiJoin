using SemiJoin;
using SemiJoin.Domain;
using System.Text.Json;

if (args.Length != 1)
{
    Console.Error.WriteLine("Only one argument must be provided");
    Environment.Exit(1);
}
var file = new FileInfo(args[0]);
if (!file.Exists)
{
    Console.Error.WriteLine("Invalid file path");
    Environment.Exit(2);
}
using var fileSteam = file.OpenRead();
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
var data = await JsonSerializer.DeserializeAsync<MatrixDto>(fileSteam, options);
if (data is null)
{
    Console.Error.WriteLine("Invalid data format");
    Environment.Exit(3);
}
Console.WriteLine("Given matrix:");
var keys = data.Value.Keys.ToList();
var matrix = data.MapToAttributeMatrix();
Console.WriteLine(matrix.ToPrintableString(keys));
var mininizer = new SemiJoinMinimizer();
var result = mininizer.Minimize(data.MapToAttributeMatrix());
Console.WriteLine("Result:");
Console.WriteLine(result.ToPrintableString(keys));
