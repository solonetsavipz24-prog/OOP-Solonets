using System;
using System.Collections.Generic;

public class LibraryItem
{
    private string _title;
    private int _publicationYear;
    private readonly string _uniqueId;

    public string Title
    {
        get => _title;
        set => _title = value;
    }

    public int PublicationYear
    {
        get => _publicationYear;
        set => _publicationYear = value;
    }

    public string UniqueId => _uniqueId;

    public LibraryItem(string title, int publicationYear, string uniqueId)
    {
        _title = title;
        _publicationYear = publicationYear;
        _uniqueId = uniqueId;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"ID: {UniqueId}, назва: {Title}, рік: {PublicationYear}");
    }
}

public class Book : LibraryItem
{
    private string _author;

    public string Author
    {
        get => _author;
        set => _author = value;
    }

    public Book(string title, int publicationYear, string uniqueId, string author)
        : base(title, publicationYear, uniqueId)
    {
        _author = author;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine($"Книга | ID: {UniqueId}, назва: {Title}, рік: {PublicationYear}, автор: {Author}");
    }
}

public class Journal : LibraryItem
{
    private int _issueNumber;

    public int IssueNumber
    {
        get => _issueNumber;
        set => _issueNumber = value;
    }

    public Journal(string title, int publicationYear, string uniqueId, int issueNumber)
        : base(title, publicationYear, uniqueId)
    {
        _issueNumber = issueNumber;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine($"Журнал | ID: {UniqueId}, назва: {Title}, рік: {PublicationYear}, випуск: {IssueNumber}");
    }
}

public class AudioBook : Book
{
    private string _narrator;

    public string Narrator
    {
        get => _narrator;
        set => _narrator = value;
    }

    public AudioBook(string title, int publicationYear, string uniqueId, string author, string narrator)
        : base(title, publicationYear, uniqueId, author)
    {
        _narrator = narrator;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine($"Аудіокнига | ID: {UniqueId}, назва: {Title}, рік: {PublicationYear}, автор: {Author}, оповідач: {Narrator}");
    }
}

public class Program
{
    public static void Main()
    {
        List<LibraryItem> items = new List<LibraryItem>
        {
            new Book("1984", 1949, "B-001", "George Orwell"),
            new Journal("IT Today", 2026, "J-001", 10),
            new AudioBook("The Hobbit", 1937, "A-001", "J. R. R. Tolkien", "Andy Serkis")
        };

        Console.WriteLine("Бібліотечні елементи:\n");
        foreach (LibraryItem item in items)
        {
            item.DisplayDetails();
        }
    }
}
