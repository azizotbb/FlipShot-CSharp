using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public static class ImageProcessor
{
    // Convert image to black and white
    public static Image<Rgba32> ConvertToGrayscale(Image<Rgba32> image)
    {
        var grayscaleImage = image.Clone();
        grayscaleImage.Mutate(ctx => ctx.Grayscale());
        return grayscaleImage;
    }

    // Flip image left to right
    public static Image<Rgba32> FlipHorizontally(Image<Rgba32> image)
    {
        var flippedImage = image.Clone();
        flippedImage.Mutate(ctx => ctx.Flip(FlipMode.Horizontal));
        return flippedImage;
    }

    // Flip image up to down
    public static Image<Rgba32> FlipVertically(Image<Rgba32> image)
    {
        var flippedImage = image.Clone();
        flippedImage.Mutate(ctx => ctx.Flip(FlipMode.Vertical));
        return flippedImage;
    }
}
