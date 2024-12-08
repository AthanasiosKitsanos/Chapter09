using System;
using Packt.Shared; // To use Viper
using System.Xml; // To use XmlWriter and so on

namespace WorkingWithStreams
{
    partial class MainProgram
    {
        static void Main(string[] args)
        {
            SectionTitle("Writting to text streams");

            // Define a file to write to.
            string textFile = Combine(Environment.CurrentDirectory, "streams.txt");

            // Create a text file and return a helpet writer
            StreamWriter text = File.CreateText(textFile);

            // Enumerate the strings, writing each one to the stream on a seperate line.
            foreach (string item in Viper.Callsigns)
            {
                text.WriteLine(item); // text.Write(item); would write every element in the array, in a line.
            }
            text.Close(); //  Release unmanaged resourses
            OutputFileInfo(textFile);

            // Writing to XML streams.
            SectionTitle("Writing to XML streams");

            // Define a file path to write to
            string xmlFile = Combine(Environment.CurrentDirectory, "streams.xml");

            // Declare variables for the filestream and xml writer
            FileStream? xmlFileStream = null;
            XmlWriter? xml = null;
            try
            {
                xmlFileStream = File.Create(xmlFile);

                // Wrap the file stream in an XML writer helper and tell it to automatically indent nested elements
                xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings {Indent = true});

                // Write the XML declaration.
                xml.WriteStartDocument();

                // Write a root element.
                xml.WriteStartElement("callsigns");

                // Enumerate the strings, writing each one to the stream.
                foreach(string item in Viper.Callsigns)
                {
                    xml.WriteStartElement("callsigns", item);
                }

                // Write the close root element.
                xml.WriteEndElement();
            }
            catch(Exception ex)
            {
                // If the path doesn't exist, the exception will be caught.
                Console.WriteLine($"{ex.GetType()} says: {ex.Message}");
            }
            finally
            {
                if(xml is not null)
                {
                    xml.Close();
                    Console.WriteLine("The XML writer's unmanaged resourses have been disposed.");
                }
                if(xmlFileStream is not null)
                {
                    xmlFileStream.Close();
                    Console.WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }
            OutputFileInfo(xmlFile);

            SectionTitle("Compressing Streams");
            Compress(algorithm: "gzip");
            Compress(algorithm: "brotli");
        }
    }
}
