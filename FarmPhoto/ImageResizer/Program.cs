using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageResizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var image = Image.FromFile(@"c:\test\logo.jpeg");
            var newImage = ScaleImage(image, 800, 800);
            newImage.Save(@"c:\test\test.jpeg", ImageFormat.Jpeg);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth/image.Width;
            var ratioY = (double) maxHeight/image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (image.Width*ratio);
            var newHeight = (int) (image.Height*ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}
    

