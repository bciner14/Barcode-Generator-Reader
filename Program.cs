using System;
using System.Drawing;
using ZXing;
using ZXing.Common;

class Program
{
    static void Main(string[] args)
    {
        string barcodeText = "123456789";
        string filePath = "barcode.png";

        // Generate and save the barcode
        GenerateBarcode(barcodeText, filePath);

        // Read the barcode from the file
        string readText = ReadBarcode(filePath);
        Console.WriteLine($"Read text from barcode: {readText}");
    }

    static void GenerateBarcode(string text, string filePath)
    {
        var barcodeWriter = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.CODE_128,
            Options = new EncodingOptions
            {
                Width = 300,
                Height = 150
            }
        };

        using (Bitmap bitmap = barcodeWriter.Write(text))
        {
            bitmap.Save(filePath);
        }
    }

    static string ReadBarcode(string filePath)
    {
        var barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            TryInverted = true,
            Options = new DecodingOptions
            {
                TryHarder = true
            }
        };

        using (Bitmap bitmap = new Bitmap(filePath))
        {
            var result = barcodeReader.Decode(bitmap);
            return result?.Text;
        }
    }
}
