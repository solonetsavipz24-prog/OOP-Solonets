using System;
using System.Collections.Generic;

public interface INotifier
{
    void Send(string message);
}

public class EmailNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

public class PushNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"Push: {message}");
    }
}

public abstract class FileProcessor
{
    public string FileName { get; }

    protected FileProcessor(string fileName)
    {
        FileName = fileName;
    }

    public void Process()
    {
        OpenFile();
        ProcessContent();
        CloseFile();
    }

    protected virtual void OpenFile()
    {
        Console.WriteLine($"Відкрито файл: {FileName}");
    }

    protected abstract void ProcessContent();

    protected virtual void CloseFile()
    {
        Console.WriteLine($"Закрито файл: {FileName}");
    }
}

public class TextFileProcessor : FileProcessor
{
    public TextFileProcessor(string fileName) : base(fileName)
    {
    }

    protected override void ProcessContent()
    {
        Console.WriteLine("Обробка текстового вмісту");
    }
}

public class XmlFileProcessor : FileProcessor
{
    public XmlFileProcessor(string fileName) : base(fileName)
    {
    }

    protected override void ProcessContent()
    {
        Console.WriteLine("Обробка XML-вмісту");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Сценарій 1: система сповіщень ===");

        List<INotifier> notifiers = new List<INotifier>
        {
            new EmailNotifier(),
            new SmsNotifier(),
            new PushNotifier()
        };

        foreach (INotifier notifier in notifiers)
        {
            notifier.Send("Самостійна робота №6");
        }

        Console.WriteLine("\n=== Сценарій 2: обробка файлів ===");

        List<FileProcessor> processors = new List<FileProcessor>
        {
            new TextFileProcessor("notes.txt"),
            new XmlFileProcessor("data.xml")
        };

        foreach (FileProcessor processor in processors)
        {
            processor.Process();
            Console.WriteLine();
        }
    }
}
