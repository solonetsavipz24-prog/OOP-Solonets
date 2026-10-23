using System.Diagnostics;
using System.Threading;

var resultLines = new List<string>
{
    "# Runtime LINQ vs PLINQ results",
    "",
    "| Elements | LINQ ms | PLINQ ms | Difference |",
    "|---:|---:|---:|---:|"
};

foreach (int size in new[] { 1_000_000, 5_000_000, 10_000_000 })
{
    var data = GenerateData(size, 42);
    var (linqMs, plinqMs, difference) = Compare(data);
    resultLines.Add($"| {size} | {linqMs} | {plinqMs} | {difference:E3} |");
}

Console.WriteLine("\nThread-safety demonstration:");
var sample = Enumerable.Range(1, 1_000_000).ToArray();
resultLines.Add("");
resultLines.Add("## Side effect test");
resultLines.Add($"Expected counter each run: {sample.Length}");

for (int attempt = 1; attempt <= 5; attempt++)
{
    int unsafeCounter = 0;
    sample.AsParallel().ForAll(_ => unsafeCounter++);
    Console.WriteLine($"Unsafe attempt {attempt}: {unsafeCounter}");
    resultLines.Add($"Unsafe attempt {attempt}: {unsafeCounter}");
}

int safeCounter = 0;
sample.AsParallel().ForAll(_ => Interlocked.Increment(ref safeCounter));
Console.WriteLine($"Expected:     {sample.Length}");
Console.WriteLine($"Safe counter: {safeCounter}");
resultLines.Add($"Safe counter (Interlocked): {safeCounter}");

string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
string resultsPath = Path.Combine(projectDirectory, "RESULTS.md");
File.WriteAllLines(resultsPath, resultLines);
Console.WriteLine($"\nRESULTS.md saved to: {resultsPath}");

static List<int> GenerateData(int count, int seed)
{
    var random = new Random(seed);
    var data = new List<int>(count);

    for (int i = 0; i < count; i++)
        data.Add(random.Next(1, 100_000));

    return data;
}

static (long LinqMs, long PlinqMs, double Difference) Compare(List<int> data)
{
    var sw = Stopwatch.StartNew();
    double linq = data
        .Where(value => value % 3 == 0)
        .Select(HeavyCalculation)
        .Sum();
    sw.Stop();
    long linqMs = sw.ElapsedMilliseconds;

    sw.Restart();
    double plinq = data
        .AsParallel()
        .Where(value => value % 3 == 0)
        .Select(HeavyCalculation)
        .Sum();
    sw.Stop();
    long plinqMs = sw.ElapsedMilliseconds;

    double difference = Math.Abs(linq - plinq);
    Console.WriteLine(
        $"{data.Count,10:N0} | LINQ {linqMs,6} ms | PLINQ {plinqMs,6} ms | diff {difference:E3}");

    return (linqMs, plinqMs, difference);
}

static double HeavyCalculation(int value)
{
    double x = value;

    for (int i = 0; i < 12; i++)
        x = Math.Sqrt(x * 1.000001 + i + 1);

    return x;
}
