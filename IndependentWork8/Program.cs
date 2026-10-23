using System.Text.RegularExpressions;

Console.WriteLine("=== Scenario 1: File metadata cache ===");
var metadataCache = new FileMetadataCache();
metadataCache.Add("report.pdf", new FileMetadata("report.pdf", 2048, DateTime.Now, "A1B2"));
bool hasReport = metadataCache.Contains("report.pdf");
long? reportSize = metadataCache.Get("report.pdf")?.Size;
Console.WriteLine($"Contains report.pdf: {hasReport}");
Console.WriteLine($"Size: {reportSize} bytes");

Console.WriteLine("\n=== Scenario 2: Tags ===");
var tags = new TagManager();
tags.AddTag("article-1", "C#");
tags.AddTag("article-1", ".NET");
tags.AddTag("article-1", "Collections");
tags.AddTag("article-2", "C#");
tags.AddTag("article-2", "LINQ");
Console.WriteLine("All tags: " + string.Join(", ", tags.GetAllUniqueTags()));
Console.WriteLine("Common: " + string.Join(", ", tags.FindCommonTags("article-1", "article-2")));

Console.WriteLine("\n=== Scenario 3: Search index ===");
var index = new SearchIndex();
index.AddDocument(1, "C# collections use Dictionary and HashSet");
index.AddDocument(2, "LINQ works with many C# collections");
index.AddDocument(3, "Python also has sets and dictionaries");
Console.WriteLine("Search 'c#': " + string.Join(", ", index.Search("c#")));
Console.WriteLine("Search 'c# collections': " + string.Join(", ", index.SearchMultiple("c#", "collections")));

public record FileMetadata(string FileName, long Size, DateTime LastModified, string Hash);

public class FileMetadataCache
{
    private readonly Dictionary<string, FileMetadata> _cache = new(StringComparer.OrdinalIgnoreCase);

    public void Add(string key, FileMetadata metadata) => _cache[key] = metadata;
    public FileMetadata? Get(string key) => _cache.TryGetValue(key, out var value) ? value : null;
    public bool Remove(string key) => _cache.Remove(key);
    public bool Contains(string key) => _cache.ContainsKey(key);
}

public class TagManager
{
    private readonly Dictionary<string, HashSet<string>> _articleTags = new(StringComparer.OrdinalIgnoreCase);

    public void AddTag(string articleId, string tag)
    {
        if (!_articleTags.TryGetValue(articleId, out var set))
        {
            set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _articleTags[articleId] = set;
        }

        set.Add(tag);
    }

    public void RemoveTag(string articleId, string tag)
    {
        if (_articleTags.TryGetValue(articleId, out var set))
            set.Remove(tag);
    }

    public HashSet<string> GetAllUniqueTags()
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var set in _articleTags.Values)
            result.UnionWith(set);
        return result;
    }

    public HashSet<string> FindCommonTags(string articleId1, string articleId2)
    {
        if (!_articleTags.TryGetValue(articleId1, out var first) ||
            !_articleTags.TryGetValue(articleId2, out var second))
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        var result = new HashSet<string>(first, StringComparer.OrdinalIgnoreCase);
        result.IntersectWith(second);
        return result;
    }
}

public class SearchIndex
{
    private readonly Dictionary<string, HashSet<int>> _invertedIndex = new(StringComparer.OrdinalIgnoreCase);

    public void AddDocument(int docId, string content)
    {
        foreach (var word in Tokenize(content))
        {
            if (!_invertedIndex.TryGetValue(word, out var docs))
            {
                docs = new HashSet<int>();
                _invertedIndex[word] = docs;
            }

            docs.Add(docId);
        }
    }

    public HashSet<int> Search(string word)
    {
        return _invertedIndex.TryGetValue(word, out var docs)
            ? new HashSet<int>(docs)
            : new HashSet<int>();
    }

    public HashSet<int> SearchMultiple(params string[] words)
    {
        if (words.Length == 0)
            return new HashSet<int>();

        var result = Search(words[0]);
        foreach (var word in words.Skip(1))
            result.IntersectWith(Search(word));
        return result;
    }

    private static IEnumerable<string> Tokenize(string text)
    {
        return Regex.Matches(text.ToLowerInvariant(), @"[\p{L}\p{N}#\+]+")
            .Select(match => match.Value)
            .Distinct();
    }
}
