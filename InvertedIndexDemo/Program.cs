using System.Diagnostics;
using System.Text.RegularExpressions;

var baseDocuments = DataFactory.CreateBaseDocuments();
var index = new InvertedIndex();
foreach (var doc in baseDocuments)
    index.AddDocument(doc);

Console.WriteLine($"Loaded {index.DocumentCount} documents");
Console.WriteLine($"Index contains {index.UniqueWords} unique words");
Console.WriteLine();

var reportLines = new List<string>
{
    "# Runtime performance results",
    "",
    "| Documents | Linear ticks | Index ticks | Found |",
    "|---:|---:|---:|---:|"
};

foreach (int size in new[] { 10, 100, 1000, 10000 })
{
    var docs = DataFactory.GenerateDocuments(size);
    var result = PerformanceTest.CompareSearchMethods(docs, "programming");
    reportLines.Add($"| {size} | {result.LinearTicks} | {result.IndexTicks} | {result.Found} |");
}

string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
string resultsPath = Path.Combine(projectDirectory, "RESULTS.md");
File.WriteAllLines(resultsPath, reportLines);
Console.WriteLine($"\nRESULTS.md saved to: {resultsPath}");

if (args.Contains("--interactive"))
{
    while (true)
    {
        Console.Write("\nEnter search query (or 'exit'): ");
        string? query = Console.ReadLine();

        if (string.Equals(query, "exit", StringComparison.OrdinalIgnoreCase))
            break;
        if (string.IsNullOrWhiteSpace(query))
            continue;

        var words = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var results = index.SearchMultipleWords(words);
        Console.WriteLine("--- Search Results (AND) ---");
        foreach (var doc in results)
            Console.WriteLine(doc);
        Console.WriteLine($"Found {results.Count} documents");
    }
}

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public override string ToString() => $"[{Id}] {Title}";
}

public class InvertedIndex
{
    private readonly Dictionary<string, HashSet<int>> _index = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<int, Document> _documents = new();

    public void AddDocument(Document doc)
    {
        _documents[doc.Id] = doc;

        foreach (var word in TextProcessor.RemoveStopWords(TextProcessor.Tokenize(doc.Content)).Distinct())
        {
            if (!_index.TryGetValue(word, out var ids))
            {
                ids = new HashSet<int>();
                _index[word] = ids;
            }

            ids.Add(doc.Id);
        }
    }

    public List<Document> SearchSingleWord(string word)
    {
        string normalized = TextProcessor.Tokenize(word).FirstOrDefault() ?? "";
        return _index.TryGetValue(normalized, out var ids)
            ? ids.Select(id => _documents[id]).ToList()
            : new List<Document>();
    }

    public List<Document> SearchMultipleWords(params string[] words)
    {
        var normalized = words
            .SelectMany(TextProcessor.Tokenize)
            .Where(word => !TextProcessor.IsStopWord(word))
            .ToArray();

        if (normalized.Length == 0)
            return new List<Document>();

        HashSet<int>? result = null;

        foreach (var word in normalized)
        {
            if (!_index.TryGetValue(word, out var ids))
                return new List<Document>();

            if (result is null)
                result = new HashSet<int>(ids);
            else
                result.IntersectWith(ids);
        }

        return result!.Select(id => _documents[id]).ToList();
    }

    public List<Document> SearchAnyWord(params string[] words)
    {
        var result = new HashSet<int>();

        foreach (var word in words.SelectMany(TextProcessor.Tokenize))
        {
            if (_index.TryGetValue(word, out var ids))
                result.UnionWith(ids);
        }

        return result.Select(id => _documents[id]).ToList();
    }

    public int DocumentCount => _documents.Count;
    public int UniqueWords => _index.Count;
}

public static class TextProcessor
{
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "a", "an", "the", "is", "are", "was", "were",
        "in", "on", "at", "to", "for", "of", "and", "or"
    };

    public static IEnumerable<string> Tokenize(string text)
    {
        return Regex.Matches(text.ToLowerInvariant(), @"[\p{L}\p{N}#\+]+")
            .Select(match => match.Value);
    }

    public static IEnumerable<string> RemoveStopWords(IEnumerable<string> words)
        => words.Where(word => !StopWords.Contains(word));

    public static bool IsStopWord(string word) => StopWords.Contains(word);
}

public static class PerformanceTest
{
    public static (long LinearTicks, long IndexTicks, int Found) CompareSearchMethods(
        List<Document> documents,
        string searchWord)
    {
        string normalized = TextProcessor.Tokenize(searchWord).First();

        var sw = Stopwatch.StartNew();
        var linear = documents
            .Where(doc => TextProcessor.Tokenize(doc.Content).Contains(normalized))
            .ToList();
        sw.Stop();
        long linearTicks = sw.ElapsedTicks;

        var index = new InvertedIndex();
        foreach (var doc in documents)
            index.AddDocument(doc);

        sw.Restart();
        var indexed = index.SearchSingleWord(searchWord);
        sw.Stop();
        long indexTicks = sw.ElapsedTicks;

        if (linear.Count != indexed.Count)
            throw new InvalidOperationException("Search results differ.");

        Console.WriteLine(
            $"{documents.Count,5} docs | linear {linearTicks,8} ticks | index {indexTicks,8} ticks | found {indexed.Count}");

        return (linearTicks, indexTicks, indexed.Count);
    }
}

public static class DataFactory
{
    private static readonly (string Title, string Content)[] Samples =
    {
        ("Introduction to C#", "C# is a modern object-oriented programming language developed by Microsoft."),
        ("Java Basics", "Java is a popular programming language known for portability and performance."),
        ("Python Overview", "Python is a high-level programming language with simple syntax."),
        ("C# Advanced Features", "C# supports LINQ async await and many modern programming paradigms."),
        ("Comparing Languages", "C# Java and Python are among the most popular programming languages today.")
    };

    public static List<Document> CreateBaseDocuments() => GenerateDocuments(5);

    public static List<Document> GenerateDocuments(int count)
    {
        var list = new List<Document>(count);

        for (int i = 0; i < count; i++)
        {
            var sample = Samples[i % Samples.Length];
            list.Add(new Document
            {
                Id = i + 1,
                Title = $"{sample.Title} #{i + 1}",
                Content = sample.Content
            });
        }

        return list;
    }
}
