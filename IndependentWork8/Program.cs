using IndependentWork8;

// Сценарій 1: кеш метаданих файлів
Console.WriteLine("=== FILE METADATA CACHE ===");

var fileCache = new FileMetadataCache();

fileCache.Add("report.pdf", new FileMetadata
{
    FileName = "report.pdf",
    Size = 250000,
    LastModified = DateTime.Now,
    Hash = "A1B2C3"
});

fileCache.Add("photo.png", new FileMetadata
{
    FileName = "photo.png",
    Size = 150000,
    LastModified = DateTime.Now,
    Hash = "D4E5F6"
});

var file = fileCache.Get("report.pdf");

Console.WriteLine($"Файл: {file.FileName}");
Console.WriteLine($"Розмір: {file.Size} байт");
Console.WriteLine($"Хеш: {file.Hash}");
Console.WriteLine($"Містить photo.png: {fileCache.Contains("photo.png")}");

fileCache.Remove("photo.png");
Console.WriteLine($"Кількість файлів: {fileCache.Count}");

// Сценарій 2: унікальні теги
Console.WriteLine("\n=== TAG MANAGER ===");

var tagManager = new TagManager();

tagManager.AddTag("article1", "CSharp");
tagManager.AddTag("article1", "DotNet");
tagManager.AddTag("article1", "Programming");

tagManager.AddTag("article2", "CSharp");
tagManager.AddTag("article2", "DotNet");
tagManager.AddTag("article2", "Web");

Console.WriteLine("Усі унікальні теги:");
Console.WriteLine(string.Join(", ",
    tagManager.GetAllUniqueTags().OrderBy(x => x)));

Console.WriteLine("Спільні теги:");
Console.WriteLine(string.Join(", ",
    tagManager.FindCommonTags("article1", "article2")));

Console.WriteLine("Тільки у першій статті:");
Console.WriteLine(string.Join(", ",
    tagManager.FindTagsOnlyInArticle("article1", "article2")));

tagManager.RemoveTag("article1", "Programming");

// Сценарій 3: пошуковий індекс
Console.WriteLine("\n=== SEARCH INDEX ===");

var searchIndex = new SearchIndex();

searchIndex.AddDocument(1, "dotnet dictionary cache");
searchIndex.AddDocument(2, "dotnet hashset collections");
searchIndex.AddDocument(3, "csharp cache metadata");

Console.WriteLine("Пошук слова dotnet:");
Console.WriteLine(string.Join(", ",
    searchIndex.Search("dotnet").OrderBy(x => x)));

Console.WriteLine("Пошук слів dotnet і dictionary:");
Console.WriteLine(string.Join(", ",
    searchIndex.SearchMultiple("dotnet", "dictionary")
        .OrderBy(x => x)));

Console.WriteLine("\nПрограму завершено успішно.");
