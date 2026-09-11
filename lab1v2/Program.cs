class Car
{
    private string brand;
    private string model;
    private int year;

    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public Car(string brand, string model, int year)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
    }

    public void Drive()
    {
        Console.WriteLine($"{Year} {Brand} {Model} їде по дорозі.");
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car("BMW", "M5", 2022);
        Car car2 = new Car("Toyota", "Camry", 2023);
        Car car3 = new Car("Audi", "A6", 2021);

        car1.Drive();
        car2.Drive();
        car3.Drive();
    }
}