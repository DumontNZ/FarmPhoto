using System;
using System.IO;
using System.Drawing;
using FarmPhoto.Domain;
using System.Drawing.Imaging;

namespace FarmPhoto.Common
{
    public class ImageScale
    {
        public static Photo ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            var ms = new MemoryStream();

            newImage.Save(ms, ImageFormat.Jpeg);

            var photo = new Photo
            {
                PhotoData = ms.ToArray(),
                Width = newWidth,
                Height = newHeight
            };

            ms.Dispose();

            return photo;
        }
    }
}
