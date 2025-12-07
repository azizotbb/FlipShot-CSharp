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

        // Create output directory if it doesn't exist
        var outputDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Screenshots");
        Directory.CreateDirectory(outputDir);

        // Take screenshot
        Console.WriteLine("📸 Taking screenshot...");
        var screenshot = await ImageCapture.CaptureScreen();

        // Save the screenshot
        var fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        var filePath = Path.Combine(outputDir, fileName);

        await screenshot.SaveAsPngAsync(filePath);

        Console.WriteLine("✅ Screenshot saved successfully!");
        Console.WriteLine($"📂 Location: {filePath}");
        Console.WriteLine($"📐 Size: {screenshot.Width} x {screenshot.Height}");

        // Clean up
        screenshot.Dispose();
    }
}