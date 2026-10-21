using System;
using System.Collections.Generic;

public static class CollectionHelper<T>
{
    public static IEnumerable<TResult> Map<TResult>(IEnumerable<T> collection, Func<T, TResult> mapper)
    {
        foreach (T item in collection)
        {
            yield return mapper(item);
        }
    }
}

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Map для List<int> ===");

        List<int> numbers = new() { 1, 2, 3, 4, 5 };
        IEnumerable<string> mappedNumbers = CollectionHelper<int>.Map(
            numbers,
            number => $"Число: {number}"
        );

        foreach (string item in mappedNumbers)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("=== Map для List<Product> ===");

        List<Product> products = new()
        {
            new Product("Клавіатура", 799m),
            new Product("Миша", 499m),
            new Product("Навушники", 1299m)
        };

        IEnumerable<decimal> prices = CollectionHelper<Product>.Map(
            products,
            product => product.Price
        );

        foreach (decimal price in prices)
        {
            Console.WriteLine($"Ціна: {price} грн");
        }

        Console.WriteLine();
        Console.WriteLine("=== Map для List<string> ===");

        List<string> words = new() { "C#", "Delegates", "Lambda" };
        IEnumerable<int> lengths = CollectionHelper<string>.Map(
            words,
            word => word.Length
        );

        foreach (int length in lengths)
        {
            Console.WriteLine($"Довжина: {length}");
        }
    }
}
