using IndependentWork7;

Console.WriteLine("Independent Work 7 - FIFO Cache");
var cache = new Cache<string, string>(3);
cache.Add("UA", "Kyiv");
cache.Add("PL", "Warsaw");
cache.Add("DE", "Berlin");
Console.WriteLine($"Count: {cache.Count}");
Console.WriteLine($"PL = {cache.Get("PL")}");
cache.Add("FR", "Paris");
Console.WriteLine($"Contains UA after overflow: {cache.ContainsKey("UA")}");
Console.WriteLine($"Contains FR: {cache.ContainsKey("FR")}");
