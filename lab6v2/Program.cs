using System;

class Shape
{
    private string color;

    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    public Shape(string color)
    {
        Color = color;
    }

    public virtual double GetArea()
    {
        return 0;
    }

    public string GetShapeType()
    {
        return "Shape";
    }
}

class Circle : Shape
{
    private double radius;

    public double Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public Circle(string color, double radius) : base(color)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public void Draw()
    {
        Console.WriteLine($"Малюємо коло кольору {Color}.");
    }
}

class Rectangle : Shape
{
    private double width;
    private double height;

    public double Width
    {
        get { return width; }
        set { width = value; }
    }

    public double Height
    {
        get { return height; }
        set { height = value; }
    }

    public Rectangle(string color, double width, double height) : base(color)
    {
        Width = width;
        Height = height;
    }

    public override double GetArea()
    {
        return Width * Height;
    }

    public void Draw()
    {
        Console.WriteLine($"Малюємо прямокутник кольору {Color}.");
    }

    public new string GetShapeType()
    {
        return "Rectangle";
    }
}

class Program
{
    static void Main()
    {
        Shape shape = new Shape("Білий");
        Circle circle = new Circle("Червоний", 5);
        Rectangle rectangle = new Rectangle("Синій", 4, 6);

        Console.WriteLine("=== Об'єкти ===");
        Console.WriteLine($"Базова фігура: колір = {shape.Color}, площа = {shape.GetArea():F2}");
        Console.WriteLine($"Коло: колір = {circle.Color}, радіус = {circle.Radius}, площа = {circle.GetArea():F2}");
        Console.WriteLine($"Прямокутник: колір = {rectangle.Color}, {rectangle.Width} x {rectangle.Height}, площа = {rectangle.GetArea():F2}");

        Console.WriteLine("\n=== Унікальні методи похідних класів ===");
        circle.Draw();
        rectangle.Draw();

        Console.WriteLine("\n=== Поліморфізм: virtual / override ===");
        Shape[] shapes =
        {
            circle,
            rectangle
        };

        foreach (Shape item in shapes)
        {
            Console.WriteLine($"{item.GetType().Name}: площа = {item.GetArea():F2}");
        }

        Console.WriteLine("\n=== Різниця між override та new ===");
        Rectangle rectangleObject = new Rectangle("Зелений", 3, 7);
        Shape rectangleAsShape = rectangleObject;

        Console.WriteLine($"Через Rectangle: {rectangleObject.GetShapeType()}");
        Console.WriteLine($"Через Shape:     {rectangleAsShape.GetShapeType()}");
    }
}
