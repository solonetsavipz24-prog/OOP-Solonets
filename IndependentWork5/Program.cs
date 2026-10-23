using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Product
{
    public string Name { get; }
    public decimal Price { get; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString() => $"Product: {Name}, {Price:F2} грн";
}

public class Customer
{
    public string Name { get; }
    public string City { get; }

    public Customer(string name, string city)
    {
        Name = name;
        City = city;
    }

    public override string ToString() => $"Customer: {Name}, місто: {City}";
}

public class Order
{
    public int Id { get; }
    public decimal Total { get; }

    public Order(int id, decimal total)
    {
        Id = id;
        Total = total;
    }

    public override string ToString() => $"Order #{Id}, сума: {Total:F2} грн";
}

public class MyCustomCollection : IEnumerable<string>
{
    private readonly List<string> _items = new List<string>();

    public void Add(string item)
    {
        _items.Add(item);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 1. Поліморфізм через Object ===");

        List<object> objects = new List<object>
        {
            new Product("Keyboard", 1800m),
            new Customer("Артем", "Рівне"),
            new Order(101, 2599m)
        };

        foreach (object item in objects)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n=== 2. Поліморфізм через IEnumerable ===");

        MyCustomCollection collection = new MyCustomCollection();
        collection.Add("C#");
        collection.Add("ООП");
        collection.Add("IEnumerable");

        foreach (string item in collection)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n=== 3. Поліморфізм через Stream ===");

        string filePath = "test.txt";
        File.WriteAllText(filePath, "FileStream example", Encoding.UTF8);

        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            ProcessStream(fileStream);
        }

        using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("MemoryStream example")))
        {
            ProcessStream(memoryStream);
        }
    }

    public static void ProcessStream(Stream stream)
    {
        byte[] buffer = new byte[8];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine($"{stream.GetType().Name}: прочитано {bytesRead} байт, початок: {text}");
    }
}
