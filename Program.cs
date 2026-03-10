using BenchmarkDotNet.Running;

namespace Lab1;

class Program
{
    static void Main(string[] args)
    {
        var benchmarkToRun = args.Length == 0 ? "insert" : args[0].Trim().ToLowerInvariant();

        switch (benchmarkToRun)
        {
            case "insert":
                BenchmarkRunner.Run<InsertKeyValueMapBenchmarks>();
                break;
            case "lookup":
                BenchmarkRunner.Run<LookupKeyValueMapBenchmarks>();
                break;
            case "remove":
                BenchmarkRunner.Run<RemoveKeyValueMapBenchmarks>();
                break;
            case "height":
                BenchmarkRunner.Run<HeightKeyValueMapBenchmarks>();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -c Release -- [insert|lookup|remove|height]");
                break;
        }
    }
}
