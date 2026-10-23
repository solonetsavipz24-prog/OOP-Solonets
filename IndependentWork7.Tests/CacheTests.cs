using Xunit;
using IndependentWork7;

namespace IndependentWork7.Tests;

public class CacheTests
{
    [Fact]
    public void Add_IncreasesCount()
    {
        var cache = new Cache<int, string>(2);
        cache.Add(1, "one");
        Assert.Equal(1, cache.Count);
    }

    [Fact]
    public void Get_ReturnsStoredValue()
    {
        var cache = new Cache<int, string>(2);
        cache.Add(1, "one");
        Assert.Equal("one", cache.Get(1));
    }

    [Fact]
    public void Overflow_RemovesOldestItem()
    {
        var cache = new Cache<int, string>(2);
        cache.Add(1, "one");
        cache.Add(2, "two");
        cache.Add(3, "three");
        Assert.False(cache.ContainsKey(1));
        Assert.True(cache.ContainsKey(2));
        Assert.True(cache.ContainsKey(3));
    }

    [Fact]
    public void Get_MissingKey_ThrowsKeyNotFoundException()
    {
        var cache = new Cache<int, string>(2);
        Assert.Throws<KeyNotFoundException>(() => cache.Get(99));
    }

    [Fact]
    public void ContainsKey_ReturnsCorrectResult()
    {
        var cache = new Cache<string, int>(2);
        cache.Add("a", 10);
        Assert.True(cache.ContainsKey("a"));
        Assert.False(cache.ContainsKey("b"));
    }

    [Fact]
    public void UpdatingExistingKey_DoesNotIncreaseCount()
    {
        var cache = new Cache<int, string>(2);
        cache.Add(1, "old");
        cache.Add(1, "new");
        Assert.Equal(1, cache.Count);
        Assert.Equal("new", cache.Get(1));
    }
}
