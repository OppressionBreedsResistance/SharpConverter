using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;

namespace SharpConverter
{
    internal class Program
    {

        static void GetCompressedAssembly(string path, string save)
        {

            // Pobranie bajtow z pliku
            byte[] bytes = File.ReadAllBytes(path);

            // Alokacja pamieci
            var CompressedStream = new MemoryStream();

            // Deflate alhorithm to compress allocated memory
            var DeflateStream = new DeflateStream(CompressedStream, CompressionMode.Compress);

            // Write File bytes to the memory
            DeflateStream.Write(bytes, 0, bytes.Length);
            DeflateStream.Dispose();

            // Get Array of bytes
            var CompressedFileBytes = CompressedStream.ToArray();
            CompressedStream.Dispose();

            // Convert bytes to base64 string
            string EncodedCompressedFile = Convert.ToBase64String(CompressedFileBytes);

            if (save == "save")
            {
                createpsscript(bytes.Length, EncodedCompressedFile);
            }

            Console.WriteLine($"Zakodowane bajty assembly to {EncodedCompressedFile}");
           
            


        }

        static void createpsscript(int bytesLength, string EncodedCompressedFile)
        {
            string template = @"
$Length = {BytesLength}

$EncodedCompressedFile = '{EncodedCompressedFile}'
# Decode from base64 and decompress stream
$DeflatedStream = New-Object System.IO.Compression.DeflateStream([IO.MemoryStream][Convert]::FromBase64String($EncodedCompressedFile), [System.IO.Compression.CompressionMode]::Decompress)

# Allocate byte array for assembly bytes
$UncompressedFileBytes = New-Object Byte[]($Length)

# Read bytes from Deflated Stream and write to byte array
$DeflatedStream.Read($UncompressedFileBytes,0, $Length)  | Out-Null

# Load the assembly from byte array
$assembly = [System.Reflection.Assembly]::Load($UncompressedFileBytes)

# Invoke your favourite function 
[HelloWorld.Class1]::Method1() # <------------- CHANGETHIS Invoke your function here! 

";

            string formattedtemplate = template.Replace("{BytesLength}", bytesLength.ToString()).Replace("{EncodedCompressedFile}", EncodedCompressedFile);



            string filePath = "script.ps1";
            File.WriteAllText(filePath, formattedtemplate);

            Console.WriteLine($"Zapisano skrypt PowerShell do pliku {filePath}");
            

        }

        static void Main(string[] args)
        {
            string kitty = @"
             /\_/\  
            ( o.o ) 
             > ^ <
            ";
            Console.WriteLine(kitty);

            if (args.Length < 1)
            { 
                Console.WriteLine("Podaj sciezke do pliku assembly!");
                return;
            }

            string filepath = @args[0];
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Taki plik nie istnieje lulz");
                return;
            }


            if (args.Length == 1)
            {
                GetCompressedAssembly(args[0], "none");
            }else
            {
                GetCompressedAssembly(args[0], args[1]);
            }


            

        }
    }
}
