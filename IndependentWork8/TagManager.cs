namespace IndependentWork8;

public class TagManager
{
    private readonly Dictionary<string, HashSet<string>> _articleTags =
        new(StringComparer.OrdinalIgnoreCase);

    public void AddTag(string articleId, string tag)
    {
        if (!_articleTags.ContainsKey(articleId))
        {
            _articleTags[articleId] =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        _articleTags[articleId].Add(tag);
    }

    public void RemoveTag(string articleId, string tag)
    {
        if (!_articleTags.TryGetValue(articleId, out var tags))
            return;

        tags.Remove(tag);

        if (tags.Count == 0)
            _articleTags.Remove(articleId);
    }

    public HashSet<string> GetAllUniqueTags()
    {
        var result = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var tags in _articleTags.Values)
            result.UnionWith(tags);

        return result;
    }

    public HashSet<string> FindCommonTags(
        string articleId1, string articleId2)
    {
        var result = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        if (_articleTags.TryGetValue(articleId1, out var first) &&
            _articleTags.TryGetValue(articleId2, out var second))
        {
            result.UnionWith(first);
            result.IntersectWith(second);
        }

        return result;
    }

    public HashSet<string> FindTagsOnlyInArticle(
        string articleId1, string articleId2)
    {
        var result = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        if (_articleTags.TryGetValue(articleId1, out var first))
            result.UnionWith(first);

        if (_articleTags.TryGetValue(articleId2, out var second))
            result.ExceptWith(second);

        return result;
    }
}
