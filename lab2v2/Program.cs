using System;

class Car
{
    private string _brand;
    private string _model;
    private int _year;

    public string Brand
    {
        get { return _brand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Марка не може бути порожньою.");

            _brand = value;
        }
    }

    public string Model
    {
        get { return _model; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Модель не може бути порожньою.");

            _model = value;
        }
    }

    public int Year
    {
        get { return _year; }
        set
        {
            if (value > DateTime.Now.Year)
                throw new ArgumentException("Рік не може бути в майбутньому.");

            _year = value;
        }
    }

    public Car() : this("BMW", " 5 Series (E39)", 2000)
    {
    }

    public Car(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }

    public void StartEngine()
    {
        Console.WriteLine($"{Year} {Brand} {Model}: двигун запущено.");
    }

    ~Car()
    {
        Console.WriteLine($"Об'єкт Car {_brand} {_model} знищено.");
    }
}

class Program
{
    static void CreateCars()
    {
        Car car1 = new Car();
        Car car2 = new Car("BMW", "M5", 2022);
        Car car3 = new Car("Toyota", "Camry", 2023);

        Console.WriteLine("Автомобілі створено:");

        Console.WriteLine($"{car1.Brand} {car1.Model}, {car1.Year}");
        car1.StartEngine();

        Console.WriteLine($"{car2.Brand} {car2.Model}, {car2.Year}");
        car2.StartEngine();

        Console.WriteLine($"{car3.Brand} {car3.Model}, {car3.Year}");
        car3.StartEngine();
    }

    static void Main()
    {
        CreateCars();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Демонстрацію життєвого циклу завершено.");
    }
}