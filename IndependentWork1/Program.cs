using System;

public class Game
{
    private string _title;
    private int _hoursPlayed;

    public string Title
    {
        get => _title;
        set => _title = value;
    }

    public int HoursPlayed => _hoursPlayed;

    public Game(string title, int hoursPlayed)
    {
        _title = title;
        _hoursPlayed = hoursPlayed;
    }

    public bool IsLongGame()
    {
        return _hoursPlayed >= 40;
    }
}

public class CoffeeOrder
{
    private string _drinkName;
    private int _quantity;

    public string DrinkName
    {
        get => _drinkName;
        set => _drinkName = value;
    }

    public int Quantity => _quantity;

    public CoffeeOrder(string drinkName, int quantity)
    {
        _drinkName = drinkName;
        _quantity = quantity;
    }

    public decimal CalculateTotal(decimal pricePerCup)
    {
        return _quantity * pricePerCup;
    }
}

public class Bicycle
{
    private string _model;
    private int _speed;

    public string Model
    {
        get => _model;
        set => _model = value;
    }

    public int Speed => _speed;

    public Bicycle(string model, int speed)
    {
        _model = model;
        _speed = speed;
    }

    public void Accelerate(int value)
    {
        if (value > 0)
        {
            _speed += value;
        }
    }
}

public class Program
{
    public static void Main()
    {
        Game game = new Game("Death Stranding", 52);
        CoffeeOrder coffee = new CoffeeOrder("Cappuccino", 2);
        Bicycle bicycle = new Bicycle("City Bike", 15);

        Console.WriteLine("=== Game ===");
        Console.WriteLine($"Назва: {game.Title}");
        Console.WriteLine($"Зіграно годин: {game.HoursPlayed}");
        Console.WriteLine($"Довга гра: {(game.IsLongGame() ? "так" : "ні")}");

        Console.WriteLine("\n=== Coffee order ===");
        Console.WriteLine($"Напій: {coffee.DrinkName}");
        Console.WriteLine($"Кількість: {coffee.Quantity}");
        Console.WriteLine($"Вартість: {coffee.CalculateTotal(65)} грн");

        Console.WriteLine("\n=== Bicycle ===");
        Console.WriteLine($"Модель: {bicycle.Model}");
        Console.WriteLine($"Початкова швидкість: {bicycle.Speed} км/год");
        bicycle.Accelerate(5);
        Console.WriteLine($"Після прискорення: {bicycle.Speed} км/год");
    }
}
