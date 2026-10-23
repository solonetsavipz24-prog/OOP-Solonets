using System.Diagnostics;

var (products, customers, sales) = DataFactory.Create();
var results = new List<string>
{
    "# Runtime performance results",
    "",
    "| Pipeline | Ticks |",
    "|---|---:|"
};

results.Add($"| Top products by category | {Analytics.TopProductsByCategory(products, sales)} |");
results.Add($"| Revenue by city | {Analytics.RevenueByCity(products, customers, sales)} |");
results.Add($"| Sales by month/year | {Analytics.SalesByMonth(products, sales)} |");
results.Add($"| Top customers by revenue | {Analytics.TopCustomers(products, customers, sales)} |");

string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
string resultsPath = Path.Combine(projectDirectory, "RESULTS.md");
File.WriteAllLines(resultsPath, results);
Console.WriteLine($"\nRESULTS.md saved to: {resultsPath}");

public record Product(int Id, string Name, string Category, decimal UnitPrice);
public record Customer(int Id, string Name, string City);
public record Sale(int Id, int ProductId, int Quantity, DateTime SaleDate, int CustomerId);

public static class Analytics
{
    public static long TopProductsByCategory(List<Product> products, List<Sale> sales)
    {
        var sw = Stopwatch.StartNew();

        var query = sales
            .GroupBy(sale => sale.ProductId)
            .Select(group => new { ProductId = group.Key, Quantity = group.Sum(x => x.Quantity) })
            .Join(products, x => x.ProductId, product => product.Id,
                (x, product) => new { product.Category, product.Name, x.Quantity })
            .GroupBy(x => x.Category)
            .Select(group => new
            {
                Category = group.Key,
                Top = group.OrderByDescending(x => x.Quantity).Take(3).ToList()
            })
            .ToList();

        sw.Stop();

        Console.WriteLine("\nTop products by category:");
        foreach (var category in query)
        {
            string top = string.Join(", ", category.Top.Select(x => $"{x.Name} ({x.Quantity})"));
            Console.WriteLine($"{category.Category}: {top}");
        }

        return sw.ElapsedTicks;
    }

    public static long RevenueByCity(
        List<Product> products,
        List<Customer> customers,
        List<Sale> sales)
    {
        var sw = Stopwatch.StartNew();

        var query = sales
            .Join(products, sale => sale.ProductId, product => product.Id,
                (sale, product) => new { Sale = sale, Product = product })
            .Join(customers, x => x.Sale.CustomerId, customer => customer.Id,
                (x, customer) => new
                {
                    customer.City,
                    Revenue = x.Sale.Quantity * x.Product.UnitPrice
                })
            .GroupBy(x => x.City)
            .Select(group => new { City = group.Key, Revenue = group.Sum(x => x.Revenue) })
            .OrderByDescending(x => x.Revenue)
            .ToList();

        sw.Stop();

        Console.WriteLine("\nRevenue by city:");
        foreach (var item in query)
            Console.WriteLine($"{item.City}: {item.Revenue:F2} грн");

        return sw.ElapsedTicks;
    }

    public static long SalesByMonth(List<Product> products, List<Sale> sales)
    {
        var sw = Stopwatch.StartNew();

        var query = sales
            .Join(products, sale => sale.ProductId, product => product.Id,
                (sale, product) => new { Sale = sale, Product = product })
            .GroupBy(x => new { x.Sale.SaleDate.Year, x.Sale.SaleDate.Month })
            .Select(group => new
            {
                group.Key.Year,
                group.Key.Month,
                Quantity = group.Sum(x => x.Sale.Quantity),
                Revenue = group.Sum(x => x.Sale.Quantity * x.Product.UnitPrice)
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        sw.Stop();

        Console.WriteLine("\nSales by month:");
        foreach (var item in query)
            Console.WriteLine($"{item.Year}-{item.Month:D2}: qty={item.Quantity}, revenue={item.Revenue:F2}");

        return sw.ElapsedTicks;
    }

    public static long TopCustomers(
        List<Product> products,
        List<Customer> customers,
        List<Sale> sales)
    {
        var sw = Stopwatch.StartNew();

        var query = sales
            .Where(sale => sale.Quantity > 0)
            .Join(products, sale => sale.ProductId, product => product.Id,
                (sale, product) => new
                {
                    sale.CustomerId,
                    Revenue = sale.Quantity * product.UnitPrice
                })
            .GroupBy(x => x.CustomerId)
            .Select(group => new { CustomerId = group.Key, Revenue = group.Sum(x => x.Revenue) })
            .Join(customers, x => x.CustomerId, customer => customer.Id,
                (x, customer) => new { customer.Name, customer.City, x.Revenue })
            .OrderByDescending(x => x.Revenue)
            .Take(5)
            .ToList();

        sw.Stop();

        Console.WriteLine("\nTop customers:");
        foreach (var item in query)
            Console.WriteLine($"{item.Name} ({item.City}): {item.Revenue:F2} грн");

        return sw.ElapsedTicks;
    }
}

public static class DataFactory
{
    public static (List<Product>, List<Customer>, List<Sale>) Create()
    {
        var random = new Random(42);
        string[] categories = { "CPU", "GPU", "RAM", "SSD", "Accessories" };
        string[] cities = { "Kyiv", "Rivne", "Lviv", "Zhytomyr", "Odesa" };

        var products = Enumerable.Range(1, 60)
            .Select(i => new Product(
                i,
                $"Product {i}",
                categories[i % categories.Length],
                random.Next(200, 30000)))
            .ToList();

        var customers = Enumerable.Range(1, 60)
            .Select(i => new Customer(i, $"Customer {i}", cities[i % cities.Length]))
            .ToList();

        var sales = Enumerable.Range(1, 100)
            .Select(i => new Sale(
                i,
                random.Next(1, 61),
                random.Next(1, 6),
                new DateTime(2026, random.Next(1, 13), random.Next(1, 28)),
                random.Next(1, 61)))
            .ToList();

        return (products, customers, sales);
    }
}
