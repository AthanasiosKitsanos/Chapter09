using System;

namespace WorkingWithFileSystems
{
    partial class Program
    {
        private static void SectionTitle(string title)
        {
            Console.WriteLine();
            ConsoleColor previousColor = Console.ForegroundColor;

            // use a color that stands out on your System.
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"***{title}***");
            Console.ForegroundColor = previousColor;
        }
    }
}

