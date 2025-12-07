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

        // Take screenshot with processing (grayscale + flip)
        Console.WriteLine("📸 Taking screenshot...");
        Console.WriteLine("🎨 Processing (black & white + flip)...");
        var processedImage = await ScreenshotManager.CaptureAndProcess(
            flipHorizontal: true,
            makeGrayscale: true
        );

        // Save image
        var filePath = await ScreenshotManager.SaveProcessedImage(
            processedImage,
            outputDir,
            isFlipped: true,
            isGrayscale: true
        );

        Console.WriteLine("✅ Screenshot saved!");
        Console.WriteLine($"📂 {filePath}");

        // Clean up memory
        processedImage.Dispose();
    }
}