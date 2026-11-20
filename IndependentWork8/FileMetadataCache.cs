namespace IndependentWork8;

public class FileMetadataCache
{
    private readonly Dictionary<string, FileMetadata> _cache =
        new(StringComparer.OrdinalIgnoreCase);

    public int Count => _cache.Count;

    public void Add(string path, FileMetadata metadata)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(metadata);

        _cache[path] = metadata;
    }

    public FileMetadata Get(string path)
    {
        if (_cache.TryGetValue(path, out var metadata))
            return metadata;

        throw new KeyNotFoundException(
            $"Файл {path} не знайдено.");
    }

    public bool Remove(string path)
    {
        return _cache.Remove(path);
    }

    public bool Contains(string path)
    {
        return _cache.ContainsKey(path);
    }
}
