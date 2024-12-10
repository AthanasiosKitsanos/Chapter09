using System;
using Packt.Shared;
using System.Text.Json; // To use JsonSerializer.

namespace ControllingJson;

class MainProgram
{
    static void Main(string[] args)
    {
        Book csharpBook = new(title: "C# 12 and .NET 8 - Modern Cross-Platform Development Fundamentals")
        {
            Author = "mark J. Price",
            PublishDate = new(year: 2023, month: 11, day: 14),
            Pages = 823,
            Created = DateTimeOffset.UtcNow
        };

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            IncludeFields = true, // Incudes all fields
            PropertyNameCaseInsensitive = true, // The property names are written non-capital
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string path = Combine(Environment.CurrentDirectory, "book.json");
        using (Stream fileStream = File.Create(path)) 
        {
            // We use Stream fileStream = File.Create(path)
            // because by having the json Options, we need to use a static Serialize method. 
            JsonSerializer.Serialize(utf8Json: fileStream, value: csharpBook, options);
        }

        Console.WriteLine("***File Info");
        Console.WriteLine($"File: {GetFileName(path)}");
        Console.WriteLine($"Path: {GetDirectoryName(path)}");
        Console.WriteLine($"Size: {new FileInfo(path).Length:N0} bytes");
        Console.WriteLine("/------------------");
        Console.WriteLine(File.ReadAllText(path));
        Console.WriteLine("------------------/");
    }
}