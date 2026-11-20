using System.Text.RegularExpressions;

namespace IndependentWork8;

public class SearchIndex
{
    private readonly Dictionary<string, HashSet<int>> _invertedIndex =
        new(StringComparer.OrdinalIgnoreCase);

    public void AddDocument(int docId, string content)
    {
        var words = Regex.Matches(content, @"[\p{L}\p{N}]+");

        foreach (Match match in words)
        {
            string word = match.Value;

            if (!_invertedIndex.ContainsKey(word))
                _invertedIndex[word] = new HashSet<int>();

            _invertedIndex[word].Add(docId);
        }
    }

    public HashSet<int> Search(string word)
    {
        if (_invertedIndex.TryGetValue(word, out var documents))
            return new HashSet<int>(documents);

        return new HashSet<int>();
    }

    public HashSet<int> SearchMultiple(params string[] words)
    {
        var result = new HashSet<int>();

        if (words.Length == 0)
            return result;

        result = Search(words[0]);

        foreach (string word in words.Skip(1))
        {
            result.IntersectWith(Search(word));

            if (result.Count == 0)
                break;
        }

        return result;
    }
}
