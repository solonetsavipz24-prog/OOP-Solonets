using System;
using System.Collections.Generic;

namespace Lab15v2
{
    public class LogEntry
    {
        public string IpAddress { get; }
        public string Message { get; }
        public DateTime Time { get; }

        public LogEntry(string ipAddress, string message)
        {
            IpAddress = ipAddress;
            Message = message;
            Time = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Time:HH:mm:ss}] {IpAddress} - {Message}";
        }
    }

    public class LogProcessor
    {
        private readonly HashSet<string> _uniqueIpAddresses = new();
        private readonly Queue<LogEntry> _logQueue = new();
        private readonly Stack<LogEntry> _viewHistory = new();

        public void AddLog(LogEntry log)
        {
            _uniqueIpAddresses.Add(log.IpAddress);
            _logQueue.Enqueue(log);
            Console.WriteLine($"Додано в чергу: {log}");
        }

        public void ShowUniqueIpAddresses()
        {
            Console.WriteLine("\nУнікальні IP-адреси:");
            foreach (string ip in _uniqueIpAddresses)
            {
                Console.WriteLine($"- {ip}");
            }

            Console.WriteLine($"Кількість унікальних IP: {_uniqueIpAddresses.Count}");
        }

        public void CheckIp(string ip)
        {
            Console.WriteLine(
                _uniqueIpAddresses.Contains(ip)
                    ? $"IP {ip} знайдено."
                    : $"IP {ip} не знайдено."
            );
        }

        public void ProcessNextLog()
        {
            if (_logQueue.Count == 0)
            {
                Console.WriteLine("Черга логів порожня.");
                return;
            }

            Console.WriteLine($"\nПерший лог у черзі (Peek): {_logQueue.Peek()}");

            LogEntry log = _logQueue.Dequeue();
            Console.WriteLine($"Оброблено (Dequeue): {log}");

            _viewHistory.Push(log);
            Console.WriteLine($"Збережено в історію перегляду.");
        }

        public void ShowQueue()
        {
            Console.WriteLine("\nПоточна черга логів:");
            if (_logQueue.Count == 0)
            {
                Console.WriteLine("Черга порожня.");
                return;
            }

            foreach (LogEntry log in _logQueue)
            {
                Console.WriteLine(log);
            }
        }

        public void ShowLastViewedLog()
        {
            if (_viewHistory.Count == 0)
            {
                Console.WriteLine("Історія порожня.");
                return;
            }

            Console.WriteLine($"\nОстанній переглянутий лог (Peek): {_viewHistory.Peek()}");
        }

        public void RemoveLastViewedLog()
        {
            if (_viewHistory.Count == 0)
            {
                Console.WriteLine("Історія порожня.");
                return;
            }

            LogEntry removed = _viewHistory.Pop();
            Console.WriteLine($"Видалено останній запис з історії (Pop): {removed}");
        }

        public void ShowHistory()
        {
            Console.WriteLine("\nІсторія переглянутих логів:");
            if (_viewHistory.Count == 0)
            {
                Console.WriteLine("Історія порожня.");
                return;
            }

            foreach (LogEntry log in _viewHistory)
            {
                Console.WriteLine(log);
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var processor = new LogProcessor();

            Console.WriteLine("=== Лабораторна робота №9 ===");
            Console.WriteLine("Варіант 2: Обробка логів\n");

            processor.AddLog(new LogEntry("192.168.0.10", "Успішний вхід"));
            processor.AddLog(new LogEntry("10.0.0.5", "Помилка авторизації"));
            processor.AddLog(new LogEntry("192.168.0.10", "Відкрито сторінку профілю"));
            processor.AddLog(new LogEntry("172.16.0.8", "Вихід із системи"));

            Console.WriteLine("\n=== HashSet: унікальні IP ===");
            processor.ShowUniqueIpAddresses();
            processor.CheckIp("10.0.0.5");
            processor.CheckIp("8.8.8.8");

            Console.WriteLine("\n=== Queue: FIFO-обробка логів ===");
            processor.ShowQueue();
            processor.ProcessNextLog();
            processor.ProcessNextLog();
            processor.ShowQueue();

            Console.WriteLine("\n=== Stack: LIFO-історія перегляду ===");
            processor.ShowHistory();
            processor.ShowLastViewedLog();
            processor.RemoveLastViewedLog();
            processor.ShowHistory();

            Console.WriteLine("\nРоботу завершено.");
        }
    }
}
