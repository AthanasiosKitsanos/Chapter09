using Packt.Shared; // To use Viper
using System.IO.Compression; // To use BrotliStream, GZipStream.
using System.Xml; // To use XmlWriter, XmlReader

namespace WorkingWithStreams
{
    partial class MainProgram
    {
        private static void Compress(string algorithm = "gzip")
        {
            // Define a file path using the algorithm as a file extention
            string filePath = Combine(Environment.CurrentDirectory, $"streams.{algorithm}");
            FileStream file = File.Create(filePath);
            Stream compressor;
            if (algorithm == "gzip")
            {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }
            else
            {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            using (compressor)
            {
                using(XmlWriter xml = XmlWriter.Create(compressor))
                {
                    xml.WriteStartDocument();
                    xml.WriteStartElement("callsigns");
                    foreach(string item in Viper.Callsigns)
                    {
                        xml.WriteElementString("callsigns", item);
                    }
                }
            } // Also closes the underlying stream
            OutputFileInfo(filePath);

            // Read the compressed file.
            Console.WriteLine("Reading the compressed file:");
            file = File.Open(filePath, FileMode.Open);
            Stream decompressor;
            if (algorithm == "gzip")
            {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }

            using (decompressor)
            using (XmlReader reader = XmlReader.Create(decompressor))
            while (reader.Read())
            {
                // Check if we are on an element node named callsigns.
                if(reader.NodeType == XmlNodeType.Element && reader.Name == "callsigns")
                {
                    reader.Read(); // Move the next inside element.
                    Console.WriteLine($"{reader.Value}"); // Read its value

                    // Alternative syntax with property pattern matching:
                    // if(reader is{NodeType: XmlNode.Element, Name: "callsigns"})
                }
            }
        }
    }
}
