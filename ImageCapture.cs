using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ImageCapture
{
    public static async Task<Image<Rgba32>> CaptureScreen()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return await CaptureScreenMacOS();
        }
        else
        {
            // For other platforms, return demo image
            return CreateDemoImage();
        }
    }
    
    private static async Task<Image<Rgba32>> CaptureScreenMacOS()
    {
        try
        {
            // Create temporary file path
            var tempPath = Path.GetTempFileName() + ".png";
            
            // Use macOS screencapture command
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "screencapture",
                    Arguments = $"-x \"{tempPath}\"", // -x prevents sound
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            
            process.Start();
            await process.WaitForExitAsync();
            
            if (process.ExitCode == 0 && File.Exists(tempPath))
            {
                // Load the captured image
                var image = await Image.LoadAsync<Rgba32>(tempPath);
                
                // Clean up temporary file
                File.Delete(tempPath);
                
                return image;
            }
            else
            {
                // If screencapture fails, return demo image
                return CreateDemoImage();
            }
        }
        catch
        {
            // If anything goes wrong, return demo image
            return CreateDemoImage();
        }
    }
    
    private static Image<Rgba32> CreateDemoImage()
    {
        var image = new Image<Rgba32>(1920, 1080);
        
        image.Mutate(ctx =>
        {
            // Fill background with blue
            ctx.Fill(Color.FromRgb(70, 130, 180));
            
            // Add demo content
            ctx.Fill(Color.Yellow, new RectangleF(100, 100, 200, 150));
            ctx.Fill(Color.Yellow, new RectangleF(550, 300, 150, 100));
        });
        
        return image;
    }
}