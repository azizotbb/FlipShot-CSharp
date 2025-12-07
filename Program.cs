using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🔥 FlipShot - Screenshot Tool");
        Console.WriteLine("============================");

        // Create output folder
        var outputDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Screenshots");
        Directory.CreateDirectory(outputDir);

        // Take screenshot
        Console.WriteLine("📸 Taking screenshot...");
        var screenshot = await ImageCapture.CaptureScreen();

        // Convert to black and white
        Console.WriteLine("🎨 Converting to black and white...");
        var processedImage = ImageProcessor.ConvertToGrayscale(screenshot);

        // Save image
        var fileName = $"Screenshot_BW_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        var filePath = Path.Combine(outputDir, fileName);

        await processedImage.SaveAsPngAsync(filePath);

        Console.WriteLine("✅ Screenshot saved!");
        Console.WriteLine($"📂 {filePath}");

        // Clean up memory
        screenshot.Dispose();
        processedImage.Dispose();
    }
}