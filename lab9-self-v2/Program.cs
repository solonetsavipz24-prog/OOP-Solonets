using Lab9SelfV2;

Console.WriteLine("===== COLLECTION UTILS =====");

// Колекція 1: числа
var numbers = new List<int> { 5, 2, 8, 1, 10 };

Console.WriteLine("\n--- List<int> ---");

var doubled = CollectionUtils.Map(numbers, n => n * 2);
Console.WriteLine("Map: " + string.Join(", ", doubled));

var maxNumber = CollectionUtils.MaxBy(numbers, n => n);
Console.WriteLine($"MaxBy: {maxNumber}");

var evenNumbers = CollectionUtils.Filter(numbers, n => n % 2 == 0);
Console.WriteLine("Filter: " + string.Join(", ", evenNumbers));

CollectionUtils.ForEach(numbers,
    n => Console.WriteLine($"Число: {n}"));

// Колекція 2: рядки
var words = new List<string>
{
    "Artem", "Rivne", "CSharp", "Git"
};

Console.WriteLine("\n--- List<string> ---");

var lengths = CollectionUtils.Map(words, w => w.Length);
Console.WriteLine("Map: " + string.Join(", ", lengths));

var longestWord = CollectionUtils.MaxBy(words, w => w.Length);
Console.WriteLine($"MaxBy: {longestWord}");

var longWords = CollectionUtils.Filter(words, w => w.Length >= 5);
Console.WriteLine("Filter: " + string.Join(", ", longWords));

CollectionUtils.ForEach(words,
    w => Console.WriteLine(w.ToUpper()));

// Колекція 3: товари
var products = new List<Product>
{
    new Product { Name = "Laptop", Price = 25000 },
    new Product { Name = "Mouse", Price = 500 },
    new Product { Name = "Keyboard", Price = 1500 }
};

Console.WriteLine("\n--- List<Product> ---");

var names = CollectionUtils.Map(products, p => p.Name);
Console.WriteLine("Map: " + string.Join(", ", names));

var expensiveProduct = CollectionUtils.MaxBy(products, p => p.Price);
Console.WriteLine($"MaxBy: {expensiveProduct.Name} - {expensiveProduct.Price} грн");

var expensive = CollectionUtils.Filter(products, p => p.Price > 1000);

Console.WriteLine("Filter:");
CollectionUtils.ForEach(expensive,
    p => Console.WriteLine($"- {p.Name}: {p.Price} грн"));

Console.WriteLine("ForEach:");
CollectionUtils.ForEach(products,
    p => Console.WriteLine($"{p.Name}: {p.Price} грн"));

Console.WriteLine("\nПрограму завершено успішно!");
