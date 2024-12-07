using System;
using Spectre.Console; // To use Table

namespace WorkingWithFileSystems
{
    partial class Program
    {
        static void Main(string[] args)
        {
            #region Handling cross-platform environments and file systems
            SectionTitle("Handling cross-platform environments and file systems");

            // Create a Spectre Console table.
            Table table = new();

            // Add two columns with markup for colors
            table.AddColumn("[blue]MEMBER[/]");
            table.AddColumn("[blue]VALUE[/]");

            // Add rows.
            table.AddRow("Path.PathSeparator", PathSeparator.ToString());
            table.AddRow("Path.DirectorySeparatorChar", DirectorySeparatorChar.ToString());
            table.AddRow("Directory.GetCurrentDirectory()", GetCurrentDirectory());
            table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
            table.AddRow("Environment.SystemDirectory", SystemDirectory);
            table.AddRow("Path.GetTempPath()", GetTempPath());
            table.AddRow("");
            table.AddRow("GetFolderPath(SpecialFolder", "");
            table.AddRow(" .System)", GetFolderPath(SpecialFolder.System));
            table.AddRow(" .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
            table.AddRow(" .MyDocuments)", GetFolderPath(SpecialFolder.MyDocuments));
            table.AddRow(" .Personal)", GetFolderPath(SpecialFolder.Personal));

            // Render the table to the console/
            AnsiConsole.Write(table);
            #endregion

            // Managing Drives.
            SectionTitle("Managing Drives");
            Table drives = new();
            drives.AddColumn("[blue]NAME[/]");
            drives.AddColumn("[blue]TYPE[/]");
            drives.AddColumn("[blue]FORMAT[/]");
            drives.AddColumn(new TableColumn("[blue]SIZE BYTES[/]").RightAligned());
            drives.AddColumn(new TableColumn("[blue]FREE SPACE[/]").RightAligned());

            foreach(DriveInfo drive in DriveInfo.GetDrives())
            {
                if(drive.IsReady)
                {
                    drives.AddRow(drive.Name, drive.DriveType.ToString(), drive.DriveFormat, drive.TotalSize.ToString("N0"), drive.AvailableFreeSpace.ToString("N0"));
                }
                else
                {
                    drives.AddRow(drive.Name, drive.DriveType.ToString(), string.Empty, string.Empty, string.Empty);
                }
            }
            AnsiConsole.Write(drives);

            // Managing Directories
            SectionTitle("Managing Directories");
            string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");
            Console.WriteLine($"Working with: {newFolder}");

            // We mut explicitly say which Exists method to use, because we statically imported both Path and Direvtory.
            Console.WriteLine($"Does it exist? {Path.Exists(newFolder)}");
            Console.WriteLine("Creating it...");
            CreateDirectory(newFolder);

            // Let's use the Directory.Exists method this time.
            Console.WriteLine($"Does it exist? {Directory.Exists(newFolder)}");
            Console.WriteLine("Confirm the directory exists and then press any key.");
            Console.ReadKey(intercept: true);
            Console.WriteLine("Deleting it...");
            Delete(newFolder, recursive: true);
            Console.WriteLine($"Does it exists? {Directory.Exists(newFolder)}");

            // Managing Files
            SectionTitle("Managing Files");

            //Define a diractory path to output files starting in the user's folder.
            string dir = Combine(GetFolderPath(SpecialFolder.Personal), "OutputFiles");
            CreateDirectory(dir);

            // Define file paths.
            string textFile = Combine(dir, "Dummy.txt");
            string backupFile = Combine(dir, "Dummy.bak");
            Console.WriteLine($"Working with: {textFile}");
            Console.WriteLine($"Does it exists? {File.Exists(textFile)}");

            // Creating a new file and write a line to it.
            StreamWriter textWriter = new StreamWriter(textFile);
            textWriter.Write("Hello, C#");
            textWriter.Close(); // Close file and release resourses.
            Console.WriteLine($"Does it exist? {File.Exists(textFile)}");

            // Copy the file and overwrite if it exists.
            File.Copy(sourceFileName: textFile, destFileName: backupFile, overwrite: true);
            Console.WriteLine($"Does backup file exist? {File.Exists(backupFile)}");
            Console.WriteLine("Confirm the files exists and then press any key");
            Console.ReadKey(intercept: true);

            // Delete the files
            File.Delete(textFile);
            Console.WriteLine($"Does it exist? {File.Exists(textFile)}");

            // Read from the file backup.
            Console.WriteLine($"Reading context of {backupFile}:");
            StreamReader textReader = File.OpenText(backupFile);
            Console.WriteLine(textReader.ReadToEnd());
            textReader.Close();

            // Managing Paths
            SectionTitle("Managing Paths");
            Console.WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
            Console.WriteLine($"File Name: {GetFileName(textFile)}");
            Console.WriteLine("File Name without Extension: {0}", arg0: GetFileNameWithoutExtension(textFile));
            Console.WriteLine($"File Extention: {GetExtension(textFile)}");
            Console.WriteLine($"Random File Name: {GetRandomFileName()}");
            Console.WriteLine($"Temporary File Name: {GetTempFileName()}");

        }
    }
}
