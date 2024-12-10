
namespace WorkingWithSerialization;

partial class MainProgram
{
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"***{title}***");
        Console.ForegroundColor = previousColor;
    }

    private static void OutputFileInfo(string path)
    {
        Console.WriteLine("***File Info***");
        Console.WriteLine($"File: {GetFileName(path)}");
        Console.WriteLine($"Path: {GetDirectoryName(path)}");
        Console.WriteLine($"Size: {new FileInfo(path).Length:N0} bytes.");
        Console.WriteLine("/------------------");
        Console.WriteLine(File.ReadAllText(path));
        Console.WriteLine("------------------/");
    }
}

