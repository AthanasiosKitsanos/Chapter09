using System;
using System.Text; // To use Encoding

namespace WorkingWithEncoding
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encodings:");
            Console.WriteLine("[1] ASCII");
            Console.WriteLine("[2] UTF-7");
            Console.WriteLine("[3] UTF-8");
            Console.WriteLine("[4] UTF-16 (Unicode)");
            Console.WriteLine("[5] UTF-32");
            Console.WriteLine("[6] Latin1");
            Console.WriteLine("[any other key] Default encoding");
            Console.WriteLine();

            Console.WriteLine("Press a number to choose an encoding.");
            ConsoleKey number = Console.ReadKey(intercept: true).Key;
            Console.WriteLine();
            Console.WriteLine();

            Encoding encoder = number switch
            {
                ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
                ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7,
                ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
                ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
                ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
                ConsoleKey.D6 or ConsoleKey.NumPad6 => Encoding.Latin1,
                _                                   => Encoding.Default
            };

            // Define a string to encode
            string messege = "Cafe $4.39";
            Console.WriteLine($"Messege to encode: {messege} Characters: {messege.Length}");

            // Encode the string into a byte array
            byte[] encoded = encoder.GetBytes(messege);

            // Check how many bytes the encoding needed
            Console.WriteLine("{0} used {1:N0} bytes.", arg0: encoder.GetType().Name, arg1: encoded.Length);
            Console.WriteLine();

            // Enumerate each byte
            Console.WriteLine("BYTE | HEX | CHAR");
            foreach(byte b in encoded)
            {
                Console.WriteLine($"{b,4} | {b,3:X} | {(char)b,4}");
            }

            // Decode the byte array back into a string and display it
            string decoded = encoder.GetString(encoded);
            Console.WriteLine($"Decoded: {decoded}");

        }
    }
}