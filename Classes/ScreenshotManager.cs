using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public static class ScreenshotManager
{
    // Take screenshot and apply processing
    public static async Task<Image<Rgba32>> CaptureAndProcess(bool flipHorizontal = false, bool flipVertical = false, bool makeGrayscale = false)
    {
        // Take screenshot
        var screenshot = await ImageCapture.CaptureScreen();

        // Apply processing
        var processedImage = screenshot.Clone();

        if (makeGrayscale)
        {
            processedImage.Dispose();
            processedImage = ImageProcessor.ConvertToGrayscale(screenshot);
        }

        if (flipHorizontal)
        {
            var temp = processedImage;
            processedImage = ImageProcessor.FlipHorizontally(temp);
            temp.Dispose();
        }

        if (flipVertical)
        {
            var temp = processedImage;
            processedImage = ImageProcessor.FlipVertically(temp);
            temp.Dispose();
        }

        // Clean up original
        screenshot.Dispose();

        return processedImage;
    }

    // Save processed image with proper filename
    public static async Task<string> SaveProcessedImage(Image<Rgba32> image, string outputDir, bool isFlipped = false, bool isGrayscale = false)
    {
        var filenameParts = new List<string> { "Screenshot" };

        if (isGrayscale)
            filenameParts.Add("BW");

        if (isFlipped)
            filenameParts.Add("Flipped");

        filenameParts.Add(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        var fileName = string.Join("_", filenameParts) + ".png";
        var filePath = Path.Combine(outputDir, fileName);

        await image.SaveAsPngAsync(filePath);

        return filePath;
    }
}
