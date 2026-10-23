using System;

public class Product
{
    private readonly int _id;
    private readonly string _name;
    private readonly decimal _price;
    private readonly string _category;
    private readonly int _stockCount;

    public int Id => _id;
    public string Name => _name;
    public decimal Price => _price;
    public string Category => _category;
    public int StockCount => _stockCount;

    public Product(int id, string name, decimal price, string category, int stockCount)
    {
        _id = id;
        _name = name;
        _price = price;
        _category = category;
        _stockCount = stockCount;
    }

    public Product(int id, string name, decimal price)
        : this(id, name, price, "Uncategorized", 0)
    {
    }

    public Product(Product other)
        : this(other.Id, other.Name, other.Price, other.Category, other.StockCount)
    {
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: {Price:F2} грн, Category: {Category}, Stock: {StockCount}";
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Створення товарів\n");

        Product product1 = new Product(101, "Laptop", 35000m, "Electronics", 7);
        Product product2 = new Product(102, "Mouse", 800m);
        Product product3 = new Product(product1);

        Console.WriteLine($"Товар 1 (основний конструктор): {product1}");
        Console.WriteLine($"Товар 2 (скорочений конструктор): {product2}");
        Console.WriteLine($"Товар 3 (конструктор копіювання): {product3}");
    }
}
