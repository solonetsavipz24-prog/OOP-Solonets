var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
var words = new List<string> { "C#", "delegates", "lambda", "LINQ" };
var products = new List<Product>
{
    new("Laptop", 25000m), new("Mouse", 500m), new("Keyboard", 1500m)
};

Console.WriteLine("Map numbers -> squares: " + string.Join(", ", CollectionUtils.Map(numbers, n => n * n)));
Console.WriteLine("Filter even: " + string.Join(", ", CollectionUtils.Filter(numbers, n => n % 2 == 0)));
Console.WriteLine("Map words -> lengths: " + string.Join(", ", CollectionUtils.Map(words, w => w.Length)));
Console.WriteLine("Longest word: " + CollectionUtils.MaxBy(words, w => w.Length));
var expensive = CollectionUtils.Filter(products, p => p.Price > 1000m);
Console.WriteLine("Expensive products:");
CollectionUtils.ForEach(expensive, p => Console.WriteLine($"- {p.Name}: {p.Price} грн"));
var maxProduct = CollectionUtils.MaxBy(products, p => p.Price);
Console.WriteLine($"Most expensive: {maxProduct.Name}");

public record Product(string Name, decimal Price);

public static class CollectionUtils
{
    public static IEnumerable<TResult> Map<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, TResult> mapper)
    {
        var result = new List<TResult>();
        foreach (var item in collection) result.Add(mapper(item));
        return result;
    }

    public static T MaxBy<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
    {
        using var enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext()) throw new InvalidOperationException("Collection is empty.");
        T maxItem = enumerator.Current;
        TKey maxKey = keySelector(maxItem);
        while (enumerator.MoveNext())
        {
            TKey key = keySelector(enumerator.Current);
            if (key.CompareTo(maxKey) > 0) { maxKey = key; maxItem = enumerator.Current; }
        }
        return maxItem;
    }

    public static IEnumerable<T> Filter<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
        var result = new List<T>();
        foreach (var item in collection) if (predicate(item)) result.Add(item);
        return result;
    }

    public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection) action(item);
    }
}
